namespace WebAppWithAuthenticationApi.Dtos;

public class SignupRequestDto
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? Position { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; }
}