using Microsoft.Build.Framework;

namespace Exercise5.Models.DTOs;

public class UpdateOrder
{
    [Required]
    public int IdOrder { get; set; }
    
    [Required]
    public int IdProduct { get; set; }
    
    [Required]
    public int Amount { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public DateTime? FulfilledAt { get; set; }
}