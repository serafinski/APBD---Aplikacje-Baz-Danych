using System.ComponentModel.DataAnnotations;

namespace Exercise5.Models.DTOs;

public class AddProductWarehouse
{
    //Wszystkie pola są wymagane!
    [Required]
    public int IdProduct { get; set; }
    
    [Required] 
    public int IdWarehouse { get; set; }
    
    
    [Required]
    // Amount musi być większe od 0!
    [Range(1,int.MaxValue,ErrorMessage = "Ilość musi być większa od 0!")]
    public int Amount { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}