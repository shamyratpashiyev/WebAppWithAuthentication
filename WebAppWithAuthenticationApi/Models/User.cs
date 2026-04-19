using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using WebAppWithAuthenticationApi.Enums;

namespace WebAppWithAuthenticationApi.Models;

public class User : IdentityUser<int>
{
    public User(int id, string name, string surname, string? position, DateTime? lastLoginDate, UserStatus status)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Position = position;
        LastLoginDate = lastLoginDate;
        Status = status;
    }

    public string Name { get; private set; }

    public string Surname { get; private set; }

    public string? Position { get; private set; }

    public DateTime? LastLoginDate { get; private set; }

    public UserStatus Status { get; private set; }

    public void SetStatus(UserStatus status)
    {
        Status = status;
    }

    public void SetLastLoginDate(DateTime? lastLoginDate)
    {
        LastLoginDate = lastLoginDate;
    }
}