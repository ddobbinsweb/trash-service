namespace TrashService.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
      
    }
    
 
}