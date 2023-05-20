using System.ComponentModel.DataAnnotations;

namespace Kolokwium1.Models;

public class Product
{
    public string Name { get; set; }
    
    public int Amount { get; set; }
}