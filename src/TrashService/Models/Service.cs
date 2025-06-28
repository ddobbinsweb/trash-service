namespace TrashService.Models;

[Index(nameof(Id))]
public class Service
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public bool IsPaid { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentReference { get; set; }
}