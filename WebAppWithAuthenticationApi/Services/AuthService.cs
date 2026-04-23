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
            await SendConfirmationLinkAsync(user.Email);
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
            var emailConfirmation = _configuration.GetSection("Ui").GetSection("EmailConfirmation");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{uiBaseUrl}/{emailConfirmation["Path"]}?{emailConfirmation["Token"]}={Uri.EscapeDataString(token)}&{emailConfirmation["UserId"]}={user.Id}";

            await _emailService.SendAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
            return;
        }

        throw new ArgumentException("User not found");
    }
    
    public async Task SendPasswordResetLinkAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var uiBaseUrl = _configuration.GetSection("Ui").GetValue<string>("BaseUrl");
            var passwordReset = _configuration.GetSection("Ui").GetSection("PasswordReset");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"{uiBaseUrl}/{passwordReset["Path"]}?{passwordReset["Token"]}={Uri.EscapeDataString(token)}&{passwordReset["UserId"]}={user.Id}";

            await _emailService.SendAsync(user.Email, "Reset Your Password",
                $"You requested a password reset. Please set a new password by <a href='{resetLink}'>clicking here</a>. " +
                "If you did not request this, you can safely ignore this email.");
            return;
        }

        throw new ArgumentException("User not found");
    }

    public async Task<bool> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }
        return false;
    }
    
    public async Task<bool> PasswordReset(string userId, string token, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);;
            return result.Succeeded;
        }
        return false;
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