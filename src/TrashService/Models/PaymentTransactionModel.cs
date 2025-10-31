namespace TrashService.Models;

public class PaymentTransaction
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; } = null!;
    public TransactionType Type { get; set; }
}

public enum TransactionType
{
    Prepayment,
    ServiceCharge,
    Refund,
    Payment
}