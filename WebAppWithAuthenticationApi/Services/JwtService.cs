using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebAppWithAuthenticationApi.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(string userName)
    {
        var jwtSettingsSection = _configuration.GetSection("JwtSettings");
        var expirationTime = jwtSettingsSection["ExpirationTimeInSec"];
        var secretKeyId = jwtSettingsSection["SecretKeyId"];

        var secretKey = _configuration[secretKeyId]; // Generated using "dotnet user-jwts key --reset" command
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var signingCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = new JwtSecurityToken(
            issuer: jwtSettingsSection["Issuer"],
            audience: jwtSettingsSection["Audience"],
            expires: DateTime.UtcNow.AddSeconds(int.Parse(expirationTime)),
            signingCredentials: signingCredential
        );
        
        return tokenHandler.WriteToken(token);
    }
}