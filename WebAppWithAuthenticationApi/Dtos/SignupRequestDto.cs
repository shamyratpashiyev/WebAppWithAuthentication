namespace WebAppWithAuthenticationApi.Dtos;

public class SignupRequestDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Position { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}