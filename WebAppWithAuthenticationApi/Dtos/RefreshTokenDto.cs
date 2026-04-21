namespace WebAppWithAuthenticationApi.Dtos;

public class RefreshTokenDto
{
    public int UserId { get; set; }
    public string Value { get; set; }
}