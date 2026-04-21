using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Services;

public interface IAuthService
{
    /// <summary>
    /// Handles authentication logic.
    /// </summary>
    Task<List<(CookieOptions cookieOptions, string tokenName, string tokenValue)>> AuthenticateAsync(LoginRequestDto request);

    /// <summary>
    /// Registers new user and authenticates it right away.
    /// </summary>
    Task<List<(CookieOptions cookieOptions, string tokenName, string tokenValue)>> RegisterAndAuthenticateAsync(SignupRequestDto request);
}