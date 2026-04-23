using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Services;

namespace WebAppWithAuthenticationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;

    public AuthController(
        IAuthService authService,
        IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginAsync([FromBody] LoginRequestDto request)
    {
        try
        {
            var res = await _authService.AuthenticateAsync(request);
            foreach (var cookie in res)
            {
                Response.Cookies.Append(cookie.tokenName, cookie.tokenValue , cookie.cookieOptions);
            }
            
            return Ok(new { message = "Login successful" });
        }
        catch
        {
            return Unauthorized("Invalid username or password.");
        }
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] SignupRequestDto request)
    {
        try
        {
            var res = await _authService.RegisterAndAuthenticateAsync(request);
            foreach (var cookie in res)
            {
                Response.Cookies.Append(cookie.tokenName, cookie.tokenValue , cookie.cookieOptions);
            }
            return Ok(new { message = "Login successful" });
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshAsync()
    {
        var refreshToken = Request.Cookies["refresh_token"];
        
        if (!string.IsNullOrEmpty(refreshToken))
        {
            var decodedJson = Convert.FromBase64String(refreshToken);
            var deserialized = JsonSerializer.Deserialize<RefreshTokenDto>(decodedJson);
            var res = await _authService.RefreshAsync(deserialized);
            foreach (var cookie in res)
            {
                Response.Cookies.Append(cookie.tokenName, cookie.tokenValue , cookie.cookieOptions);
            }

            return Ok(new { message = "Login successful" });
        }
        return Unauthorized("Invalid refresh token.");
    }
    
    [HttpPost("send-confirmation-link/{email}")]
    public async Task<IActionResult> SendConfirmationLinkAsync(string email)
    {
        try
        {
            await _authService.SendConfirmationLinkAsync(email);
        }
        catch (ArgumentException _)
        {
            return NotFound("User not found");
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }

        return Ok(new { message = "Confirmation link sent successfully." });
    }
    
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail()
    {
        var emailConfirmation = _configuration.GetSection("Ui").GetSection("EmailConfirmation");
        var userId = Request.Query[emailConfirmation["UserId"]];
        var token = Request.Query[emailConfirmation["Token"]];
        try
        {
            var successfullyConfirmed = await _authService.ConfirmEmail(userId, token);
            if (successfullyConfirmed)
            {
                return Ok("Email confirmed successfully!");
            }
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }

        return BadRequest("Invalid token or user ID");
    }
}