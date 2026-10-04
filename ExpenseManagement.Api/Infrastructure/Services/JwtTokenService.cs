using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExpenseManagement.Api.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseManagement.Api.Infrastructure.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(User user)
    {
       var claims = new[]
       {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

       var jwtKey = _configuration["Jwt:Key"]
                    ?? throw new InvalidOperationException("JWT key is not configured.");
       var key = new SymmetricSecurityKey(
           Encoding.UTF8.GetBytes(jwtKey)
       );
       var expiresInMinutes = _configuration["Jwt:ExpiresInMinutes"]
                              ?? throw new InvalidOperationException("JWT expiration is not configured.");
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(expiresInMinutes)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
       }
    }
