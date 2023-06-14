using System.ComponentModel.DataAnnotations;

namespace Exercise8.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string HashedPassword { get; set; } = null!;
}