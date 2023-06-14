using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolokwium.Models;

[Table("Status")]
public class Status
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)] 
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Order> Orders { get; set; } = null!;
}