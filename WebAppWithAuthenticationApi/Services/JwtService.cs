using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwt(User user, bool rememberMe = false)
    {
        var jwtSettingsSection = _configuration.GetSection("JwtSettings");
        var expirationTimeInSec = jwtSettingsSection["ExpirationTimeInSec"];
        var expirationTimeInDays = jwtSettingsSection["RememberMeExpirationTimeInDays"];
        var secretKeyId = jwtSettingsSection["SecretKeyId"];

        var secretKey = _configuration[secretKeyId]; // Generated using "dotnet user-jwts key --reset" command
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()), // 'Sub' is the standard for User ID
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique ID for this specific token
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettingsSection["Issuer"],
            audience: jwtSettingsSection["Audience"],
            expires: rememberMe 
                ? DateTime.UtcNow.AddDays(int.Parse(expirationTimeInDays)) 
                : DateTime.UtcNow.AddSeconds(int.Parse(expirationTimeInSec)),
            signingCredentials: signingCredential,
            claims: claims
        );
        
        return tokenHandler.WriteToken(token);
    }
    
    
}