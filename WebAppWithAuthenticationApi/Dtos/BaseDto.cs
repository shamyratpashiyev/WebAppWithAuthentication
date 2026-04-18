namespace WebAppWithAuthenticationApi.Controllers;

public class BaseDto
{
    public BaseDto(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}