using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
    private readonly IEmailService _emailService;

    public AuthService(
        UserManager<User> userManager,
        IHostEnvironment hostEnvironment,
        IJwtService jwtService,
        IConfiguration configuration,
        AppDbContext appDbContext,
        IEmailService emailService)
    {
        _userManager = userManager;
        _hostEnvironment = hostEnvironment;
        _jwtService = jwtService;
        _configuration = configuration;
        _appDbContext = appDbContext;
        _emailService = emailService;
    }
    
    public async Task<List<(string tokenName, string tokenValue, CookieOptions cookieOptions)>> AuthenticateAsync(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var result = new List<(string tokenName, string tokenValue, CookieOptions cookieOptions)>();
            var accessCookie = GenerateAccessCookie(user);
            result.Add(accessCookie);
            
            if (request.RememberMe)
            {
                var refreshCookie = GenerateRefreshCookie(user);
                result.Add(refreshCookie);
            }

            user.SetLastLoginDate(DateTime.Now);
            await _userManager.UpdateAsync(user);

            return result;
        }

        throw new Exception("Invalid username or password.");
    }
    
    public async Task<List<(string tokenName, string tokenValue, CookieOptions cookieOptions)>> RegisterAndAuthenticateAsync(SignupRequestDto request)
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

    public async Task<List<(string tokenName, string tokenValue, CookieOptions cookieOptions)>> RefreshAsync(RefreshTokenDto input)
    {
        var user = await _userManager.FindByIdAsync(input.UserId.ToString());
        if (user != null && user.RefreshToken == input.Value && user.RefreshTokenExpirationDate > DateTime.UtcNow)
        {
            var accessCookie = GenerateAccessCookie(user);
            var refreshCookie = GenerateRefreshCookie(user);
            await _userManager.UpdateAsync(user);
            return new List<(string tokenName, string tokenValue, CookieOptions cookieOptions)>()
            {
                accessCookie,
                refreshCookie
            };
        }
        throw new Exception("Error refreshing the token");
    }

    public async Task SendConfirmationLinkAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var uiBaseUrl = _configuration.GetSection("Ui").GetValue<string>("BaseUrl");
            var ui = _configuration.GetSection("Ui");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{uiBaseUrl}/{ui["EmailConfirmationPath"]}?{ui["UserId"]}={user.Id}&{ui["Token"]}={Uri.EscapeDataString(token)}";

            await _emailService.SendAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
        }

        throw new ArgumentException("User not found");
    }

    private (string tokenName, string tokenValue, CookieOptions cookieOptions) GenerateAccessCookie(User user)
    {
        var token = _jwtService.GenerateJwt(user);
        var expirationTimeInSec = _configuration.GetSection("JwtSettings").GetValue<int>("ExpirationTimeInSec");
            
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = !_hostEnvironment.IsDevelopment(),
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddSeconds(expirationTimeInSec)
        };
        return ("access_token", token, cookieOptions);
    }
    
    private (string tokenName, string tokenValue, CookieOptions cookieOptions) GenerateRefreshCookie(User user)
    {
        var token = GenerateRefreshToken(user.Id);
        var expirationTimeInDays = _configuration.GetSection("JwtSettings").GetValue<int>("RefreshExpirationTimeInDays");
        var tokenExpirationDate = DateTime.UtcNow.AddDays(expirationTimeInDays);
                
        var tokenCookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = !_hostEnvironment.IsDevelopment(),
            SameSite = SameSiteMode.Strict,
            Expires = tokenExpirationDate
        };
        user.SetRefreshToken(token.tokenValue, tokenExpirationDate);
        return ("refresh_token", token.serializedString, tokenCookieOptions);
    }
    
    private (string tokenValue, string serializedString) GenerateRefreshToken(int userId)
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        var secureValue = Convert.ToBase64String(randomBytes);

        var container = new RefreshTokenDto() 
        { 
            UserId = userId, 
            Value = secureValue 
        };

        var jsonString = JsonSerializer.Serialize(container);
        var jsonBytes = Encoding.UTF8.GetBytes(jsonString);
        return (secureValue, Convert.ToBase64String(jsonBytes));
    }
}