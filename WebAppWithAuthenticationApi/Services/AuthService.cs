using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppWithAuthenticationApi.Data;
using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Enums;
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
    
    public async Task<List<(CookieOptions cookieOptions, string tokenName, string tokenValue)>> AuthenticateAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var accessToken = _jwtService.GenerateJwt(user);
            var expirationTimeInSec = _configuration.GetSection("JwtSettings").GetValue<int>("ExpirationTimeInSec");
            var result = new List<(CookieOptions cookieOptions, string tokenName, string tokenValue)>();
            
            var accessTokenCookieOptions = new CookieOptions
            {
                HttpOnly = true,        // Prevents XSS (JavaScript can't read it)
                Secure = !_hostEnvironment.IsDevelopment(),          // Only sent over HTTPS
                SameSite = SameSiteMode.Strict, // Prevents CSRF
                Expires = DateTime.UtcNow.AddSeconds(expirationTimeInSec) // Match your JWT expiration
            };
            
            if (request.RememberMe)
            {
                var refreshToken = GenerateRefreshToken();
                var rememberMeExpirationTimeInDays = _configuration.GetSection("JwtSettings").GetValue<int>("RefreshExpirationTimeInDays");
                var refreshTokenExpirationDate = DateTime.UtcNow.AddDays(rememberMeExpirationTimeInDays);
                
                var refreshTokenCookieOptions = new CookieOptions
                {
                    HttpOnly = true,        // Prevents XSS (JavaScript can't read it)
                    Secure = !_hostEnvironment.IsDevelopment(),          // Only sent over HTTPS
                    SameSite = SameSiteMode.Strict, // Prevents CSRF
                    Expires = refreshTokenExpirationDate // Match your JWT expiration
                };
                user.SetRefreshToken(refreshToken, refreshTokenExpirationDate);
                result.Add((refreshTokenCookieOptions, "refresh_token", refreshToken));
            }

            user.SetLastLoginDate(DateTime.Now);
            await _userManager.UpdateAsync(user);
            
            result.Add((accessTokenCookieOptions, "access_token", accessToken));

            return result;
        }

        throw new Exception("Invalid username or password.");
    }
    
    public async Task<List<(CookieOptions cookieOptions, string tokenName, string tokenValue)>> RegisterAndAuthenticateAsync(SignupRequestDto request)
    {
        var user = new User(request.Email, request.Name, request.Surname, request.Position, DateTime.Now,
            UserStatus.Unverified);
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            var loginRequest = new LoginRequestDto(){ Email = request.Email, Password = request.Password, RememberMe = request.RememberMe};
            return await AuthenticateAsync(loginRequest);
        }

        throw new Exception("Error creating user");
    }
    
    private string GenerateRefreshToken()
    {
        // 1. Create a byte array to hold the random numbers
        // 32 bytes (256 bits) or 64 bytes (512 bits) is standard for security
        var randomNumber = new byte[64];

        using (var rng = RandomNumberGenerator.Create())
        {
            // 2. Fill the array with cryptographically strong random bytes
            rng.GetBytes(randomNumber);
        
            // 3. Convert to a URL-friendly string
            return Convert.ToBase64String(randomNumber);
        }
    }
}