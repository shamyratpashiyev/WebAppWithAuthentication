namespace WebAppWithAuthenticationApi.Services;

public interface IJwtService
{
    /// <summary>
    /// Generates JwtToken based on UserName.
    /// </summary>
    string Generate(string userName);
}