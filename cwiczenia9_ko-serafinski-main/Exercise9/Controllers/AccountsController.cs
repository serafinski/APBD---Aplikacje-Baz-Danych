using Exercise8.Models.DTOs;
using Exercise8.Services;
using Microsoft.AspNetCore.Mvc;

namespace Exercise8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAccountsService _accountService;

    public AccountsController(IConfiguration configuration, IAccountsService accountService)
    {
        _configuration = configuration;
        _accountService = accountService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto dto)
    {
        var user = _accountService.GetUser(dto.Username);

        if (user == null || !_accountService.VerifyHashedPassword(user, dto.Password))
        {
            return Unauthorized("Wrong username or password!");
        }
        
        var token = _accountService.GenerateToken(
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Audience"],
            15,
            false
        );

        var refreshToken = _accountService.GenerateToken(
            _configuration["JWT:RefKey"],
            _configuration["JWT:RefIssuer"],
            _configuration["JWT:RefAudience"],
            3,
            true
        );

        return Ok(new LoginResponseDto
        {
            Token = token,
            RefreshToken = refreshToken
        });
    }

    [HttpPost("refresh")]
    public IActionResult RefreshToken(RefreshTokenDto dto)
    {
        var accessToken = _accountService.ValidateRefreshToken(
            dto.RefreshToken, 
            _configuration["JWT:RefKey"], 
            _configuration["JWT:RefIssuer"], 
            _configuration["JWT:RefAudience"]
        );

        if (accessToken == null)
        {
            return Unauthorized();
        }

        return Ok(new { AccessToken = accessToken });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUser newUser)
    {
        if (await _accountService.RegisterUser(newUser))
        {
            return Ok("Registration was successful!");
        }

        return BadRequest("Username already exist!");
    }
}

}
