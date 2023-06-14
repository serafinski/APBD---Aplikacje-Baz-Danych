using Microsoft.Build.Framework;

namespace Exercise8.Models.DTOs;

public class LoginRequestDto
{
    [Required] 
    public string Username { get; set; } = null!;
    
    [Required] 
    public string Password { get; set; } = null!;
}