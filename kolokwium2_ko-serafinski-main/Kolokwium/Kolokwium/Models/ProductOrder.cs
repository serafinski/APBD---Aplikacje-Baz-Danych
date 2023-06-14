using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Models;

[Table("Product_Order"), PrimaryKey(nameof(ProductId), nameof(OrderId))]
public class ProductOrder
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    
    [Required]
    public int Amount { get; set; }
    
    [ForeignKey(nameof(OrderId))] 
    public virtual Order Order { get; set; } = null!;
    
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } = null!;
}