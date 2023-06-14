using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Exercise8.Models;
using Exercise8.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Exercise8.Services;

public interface IAccountsService
{
    User GetUser(string username);
    bool VerifyHashedPassword(User user, string password);
    string GenerateToken(string key, string issuer, string audience, int expires, bool isRefreshToken);
    string ValidateRefreshToken(string token, string refKey, string refIssuer, string refAudience);
    Task<bool> RegisterUser(RegisterUser newUser);
}

public class AccountService : IAccountsService
{
    private readonly MyDbContext _dbContext;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public AccountService(MyDbContext dbContext)
    {
        _dbContext = dbContext;
        _passwordHasher = new PasswordHasher<User>();
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    public User GetUser(string username)
    {
        return _dbContext.Users.SingleOrDefault(u => u.Name.ToLower() == username.ToLower());
    }

    public bool VerifyHashedPassword(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }

    public string GenerateToken(string key, string issuer, string audience, int expires, bool isRefreshToken)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = issuer,
            Audience = audience,
            Expires = isRefreshToken ? DateTime.UtcNow.AddDays(expires) : DateTime.UtcNow.AddMinutes(expires),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256
            )
        };
        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    public string ValidateRefreshToken(string token, string refKey, string refIssuer, string refAudience)
    {
        try
        {
            _tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = refIssuer,
                ValidAudience = refAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refKey))
            }, out SecurityToken validatedToken);
            
            var userClaims = ((JwtSecurityToken)validatedToken).Claims;
            var identity = new ClaimsIdentity(userClaims);

            return GenerateToken(
                refKey, 
                refIssuer, 
                refAudience, 
                15, 
                false);
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> RegisterUser(RegisterUser newUser)
    {
        var userExist = await _dbContext.Users.AnyAsync(x => x.Name == newUser.Name);

        if (userExist)
        {
            return false;
        }

        var user = new User
        {
            Name = newUser.Name,
            HashedPassword = _passwordHasher.HashPassword(null, newUser.Password)
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
