using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Models;

[Table("Product")]
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)] 
    public string Name { get; set; } = null!;

    [Required, Precision(10,2)] 
    public double Price { get; set; }
    
    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = null!;
}