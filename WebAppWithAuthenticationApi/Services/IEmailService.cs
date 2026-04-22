namespace WebAppWithAuthenticationApi.Services;

public interface IEmailService
{
    /// <summary>
    /// Sends email using app credentials.
    /// </summary>
    Task SendAsync(string toEmail, string subject, string body);
}