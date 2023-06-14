namespace Kolokwium.Models.DTOs;

public class AddClientOrder
{
    public DateTime CreatedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    public ICollection<GetProductsForOrder> GetProductsForOrders { get; set; } = null!;
}
public class GetProductsForOrder
{
    public int Id { get; set; } 
    public int Amount { get; set; }
}