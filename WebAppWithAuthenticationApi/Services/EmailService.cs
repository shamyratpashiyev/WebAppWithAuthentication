using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace WebAppWithAuthenticationApi.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendAsync(string toEmail, string subject, string body)
    {
        var appEmailAddr = _configuration.GetSection("EmailSettings").GetValue<string>("Email");
        var appEmailPassword = _configuration.GetSection("EmailSettings").GetValue<string>("Password");
        var emailHost = _configuration.GetSection("EmailSettings").GetValue<string>("Host");
        var emailPort = _configuration.GetSection("EmailSettings").GetValue<int>("Port");
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(appEmailAddr));
        email.To.Add(MailboxAddress.Parse(toEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        try
        {
            await smtp.ConnectAsync(emailHost, emailPort, SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(appEmailAddr, appEmailPassword);

            await smtp.SendAsync(email);
        }
        finally
        {
            await smtp.DisconnectAsync(true);
        }
    }
}