using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Services;

public interface IAuthService
{
    Task<(CookieOptions cookieOptions, string token)> AuthenticateAsync(LoginRequestDto request);
}