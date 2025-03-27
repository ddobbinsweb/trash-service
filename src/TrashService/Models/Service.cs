namespace TrashService.Models;

public class Service
{
    public int Id { get; set; }
    public DateTime Date { get; set; } // Date of service (Wednesdays)
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } // Navigation property
}