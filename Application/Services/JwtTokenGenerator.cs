using Application.Authentication;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly AuthenticationSettings authenticationSettings;

    public JwtTokenGenerator(AuthenticationSettings authenticationSettings)
    {
        this.authenticationSettings = authenticationSettings;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim("uid", user.Id.ToString()),
            new Claim("userName", $"{user.FirstName} {user.LastName}"),
            new Claim("email", $"{user.Email}"),
            new Claim("userRole", $"{user.Role.Name}"),
            new Claim("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
            authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred
            );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
