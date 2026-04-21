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

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
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
}