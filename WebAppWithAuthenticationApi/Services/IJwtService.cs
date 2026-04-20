using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Services;

public interface IJwtService
{
    /// <summary>
    /// Generates JwtToken based on provided credentials.
    /// </summary>
    string GenerateJwt(User user, bool rememberMe = false);
}