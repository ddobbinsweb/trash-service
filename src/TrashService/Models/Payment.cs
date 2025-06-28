namespace TrashService.Models;

[Index(nameof(Id))]
public class Payment
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!; // Navigation property
    public decimal Amount { get; set; } // Amount paid
    public DateTime Date { get; set; }
}