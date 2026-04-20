using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Models;
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
    public async Task<ActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var res = await _authService.AuthenticateAsync(request);
            Response.Cookies.Append("access_token", res.token, res.cookieOptions);
            return Ok(new { message = "Login successful" });
        }
        catch
        {
            return Unauthorized("Invalid username or password.");
        }
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");
        return Ok(new { message = "Logged out" });
    }
}