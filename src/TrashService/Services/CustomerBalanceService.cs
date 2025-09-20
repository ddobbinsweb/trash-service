namespace TrashService.Services;

public class CustomerBalanceService(AppDbContext dbContext)
{
    private const decimal DefaultServicePrice = 2.00m;

    public async Task AddPrepaymentAsync(int customerId, decimal amount)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var customer = await dbContext.Customers
                .Include(c => c.Services.Where(s => !s.IsPaid))
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (customer == null)
                throw new ArgumentException("Customer not found");

            // Add the prepayment to the customer's balance
            customer.Balance += amount;

            // Record the transaction
            var paymentTransaction = new PaymentTransaction
            {
                CustomerId = customerId,
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                Description = $"Prepayment added",
                Type = TransactionType.Prepayment
            };
            dbContext.PaymentTransactions.Add(paymentTransaction);

            // If there are any unpaid services, attempt to pay them automatically
            await ProcessPendingServices(customer);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task ProcessPendingServices(Customer customer)
    {
        var unpaidServices =  customer.Services
            .Where(s => !s.IsPaid)
            .OrderBy(s => s.Date).ToList();

        foreach (var service in unpaidServices)
        {
            if (Math.Abs(customer.Balance) >= service.Price)
            {
                customer.Balance -= service.Price;
                service.IsPaid = true;
                service.PaymentDate = DateTime.UtcNow;
                service.PaymentReference = $"AutoPaid-{DateTime.UtcNow:yyyyMMddHHmmss}";

                // Record the transaction
                var transaction = new PaymentTransaction
                {
                    CustomerId = customer.Id,
                    Amount = -service.Price,
                    TransactionDate = DateTime.UtcNow,
                    Description = $"Service charge for date {service.Date:d}",
                    Type = TransactionType.ServiceCharge
                };
                dbContext.PaymentTransactions.Add(transaction);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                break; // Stop if we don't have enough money
            }
        }
    }

    public async Task<decimal> GetBalanceAsync(int customerId)
    {
        var customer = await dbContext.Customers
            .FirstOrDefaultAsync(c => c.Id == customerId);
        return customer?.Balance ?? 0;
    }

    public async Task<List<PaymentTransaction>> GetTransactionsForCustomer(Customer customer)
    {
        var transactions = new List<PaymentTransaction>();
        
        // Get all customer's services that don't have matching payment transactions
        var unpaidServices = await dbContext.Services
            .Where(s => s.CustomerId == customer.Id && !s.IsPaid)
            .OrderBy(s => s.Date)
            .ToListAsync();

        // Get existing payment transactions
        var existingTransactions = await dbContext.PaymentTransactions
            .Where(t => t.CustomerId == customer.Id)
            .OrderBy(t => t.TransactionDate)
            .ToListAsync();
        
        transactions.AddRange(existingTransactions);

        // Add pending service charges for unpaid services
        foreach (var service in unpaidServices)
        {
            transactions.Add(new PaymentTransaction
            {
                CustomerId = customer.Id,
                Amount = service.Price,
                TransactionDate = service.Date,
                Description = $"Pending payment for service on {service.Date:d}",
                Type = TransactionType.ServiceCharge
            });
        }

        return transactions.OrderByDescending(t => t.TransactionDate).ToList();
    }
    public async Task CreateServicesAndUpdateBalances(List<int> customerIds, DateTime serviceDate)
    {
        var services = customerIds.Select(customerId => new Service
        {
            CustomerId = customerId,
            Date = serviceDate.ToUniversalTime(),
            Price = DefaultServicePrice,
            IsPaid = false
        });

        dbContext.Services.AddRange(services);

        // Update customer balances
        foreach (var customerId in customerIds)
        {
            var customer = await dbContext.Customers.FindAsync(customerId);
            if (customer != null)
            {
                customer.Balance -= DefaultServicePrice;
            }
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateCustomerBalance(int customerId)
    {
        var customer = await dbContext.Customers
            .Include(c => c.Services)
            .FirstOrDefaultAsync(c => c.Id == customerId);

        if (customer == null)
            throw new ArgumentException("Customer not found");

        decimal balance = 0;

        // Subtract unpaid service prices
        foreach (var service in customer.Services)
        {
            if (!service.IsPaid)
            {
                balance -= service.Price;
            }
        }

        customer.Balance = balance;
        await dbContext.SaveChangesAsync();
    }

}