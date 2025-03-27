using TrashService.Enums;

namespace TrashService.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; }
    public PaymentType PaymentType { get; set; } // Prepay or Postpay
    public decimal Balance { get; set; } // Negative for credit, positive for debt
}