using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Models;

[Table("Client")]
public class Client
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;

    
    //public virtual ICollection<Order> Orders { get; set; } = null!;
}