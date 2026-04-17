using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using WebAppWithAuthenticationApi.Enums;

namespace WebAppWithAuthenticationApi.Models;

public class User : IdentityUser<int>
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string? Position { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public UserStatus Status { get; set; }
}