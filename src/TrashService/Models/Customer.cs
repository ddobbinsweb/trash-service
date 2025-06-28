using TrashService.Enums;

namespace TrashService.Models;
[Index(nameof(Id))]
public class Customer
{
    
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public PaymentType PaymentType { get; set; } // Prepay or Postpay
    public decimal Balance { get; set; } // Negative for credit, positive for debt
    public ICollection<Service> Services { get; set; } = [];
}