using System.ComponentModel.DataAnnotations;

namespace Kolokwium1.Models;

public class OrderRequest
{
    [Required]
    public List<OrderProduct> Products { get; set; }
}