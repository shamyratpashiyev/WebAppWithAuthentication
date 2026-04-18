using WebAppWithAuthenticationApi.Controllers;
using WebAppWithAuthenticationApi.Enums;

namespace WebAppWithAuthenticationApi.Dtos;

public class UserDto : BaseDto
{
    public UserDto(int id, string name, string surname, string? email, 
        string? position, DateTime? lastLoginDate, UserStatus status) :  base(id)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Position = position;
        LastLoginDate = lastLoginDate;
        Status = status;
    }
    
    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Email { get; set; }

    public string? Position { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public UserStatus Status { get; set; }
}