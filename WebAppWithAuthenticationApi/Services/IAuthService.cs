using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Services;

public interface IAuthService
{
    /// <summary>
    /// Handles authentication logic.
    /// </summary>
    Task<List<(string tokenName, string tokenValue, CookieOptions cookieOptions)>> AuthenticateAsync(LoginRequestDto request);

    /// <summary>
    /// Registers new user and authenticates it right away.
    /// </summary>
    Task<List<(string tokenName, string tokenValue, CookieOptions cookieOptions)>> RegisterAndAuthenticateAsync(SignupRequestDto request);

    /// <summary>
    /// Validates refresh token and issues new access and refresh tokens. 
    /// </summary>
    Task<List<(string tokenName, string tokenValue, CookieOptions cookieOptions)>> RefreshAsync(RefreshTokenDto input);

    /// <summary>
    /// Sends email confirmation link.
    /// </summary>
    Task SendConfirmationLinkAsync(string email);

    /// <summary>
    /// Confirms user email by provided credentials.
    /// </summary>
    Task<bool> ConfirmEmail(string userId, string token);

    /// <summary>
    /// Sends a unique password reset link to provided email address.
    /// </summary>
    Task SendPasswordResetLinkAsync(string email);

    /// <summary>
    /// Reset a password of a user via unique password reset link.
    /// </summary>
    Task<bool> PasswordReset(string userId, string token, string newPassword);
}