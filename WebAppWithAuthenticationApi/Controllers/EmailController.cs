using Microsoft.AspNetCore.Mvc;
using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Services;

namespace WebAppWithAuthenticationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;
    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendAsync([FromBody]EmailDto input)
    {
        await _emailService.SendAsync(input.ToEmail, input.Subject, input.Body);
        return Ok();
    }
}