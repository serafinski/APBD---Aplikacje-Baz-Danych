namespace Kolokwium.Models.DTOs;

public class GetClientOrder
{
    public int OrderId { get; set; }
    public string ClientsLastName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    public string Status { get; set; } = null!;
    
    public ICollection<GetProducts> Products { get; set; } = null!;
}

public class GetProducts
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int Amount { get; set; }
}