namespace TrashService.Models;

public class Payment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int CustomerId { get; set; }
    public decimal Amount { get; set; } // Amount paid
    public Customer Customer { get; set; } // Navigation property
}