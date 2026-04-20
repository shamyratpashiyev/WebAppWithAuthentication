using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppWithAuthenticationApi.Data;
using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _appDbContext;

    public AuthService(
        UserManager<User> userManager,
        IHostEnvironment hostEnvironment,
        IJwtService jwtService,
        IConfiguration configuration,
        AppDbContext appDbContext)
    {
        _userManager = userManager;
        _hostEnvironment = hostEnvironment;
        _jwtService = jwtService;
        _configuration = configuration;
        _appDbContext = appDbContext;
    }
    
    public async Task<(CookieOptions cookieOptions, string token)> AuthenticateAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var token = _jwtService.GenerateJwt(user);
            var expirationTimeInSec = _configuration.GetSection("JwtSettings")["ExpirationTimeInSec"];

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,        // Prevents XSS (JavaScript can't read it)
                Secure = !_hostEnvironment.IsDevelopment(),          // Only sent over HTTPS
                SameSite = SameSiteMode.Strict, // Prevents CSRF
                Expires = DateTime.UtcNow.AddSeconds(int.Parse(expirationTimeInSec)) // Match your JWT expiration
            };

            user.SetLastLoginDate(DateTime.UtcNow);
            await _userManager.UpdateAsync(user);

            return (cookieOptions, token);
        }

        throw new Exception("Invalid username or password.");
    }
}