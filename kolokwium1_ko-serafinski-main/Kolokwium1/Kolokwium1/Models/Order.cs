using System.ComponentModel.DataAnnotations;

namespace Kolokwium1.Models;

public class Order
{
    public int OrderID { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public List<Product> Products { get; set; }
}
