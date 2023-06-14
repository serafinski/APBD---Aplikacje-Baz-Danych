using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium.Models;

[Table("Order")]
public class Order
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    
    
    public int ClientId { get; set; }
    public int StatusId { get; set; }
    
    [ForeignKey(nameof(ClientId))]
    public virtual Client Client { get; set; } = null!;
    
    [ForeignKey(nameof(StatusId))]
    public virtual Status Status { get; set; } = null!;
    
    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = null!;
}