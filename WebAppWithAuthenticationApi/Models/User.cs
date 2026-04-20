using Microsoft.AspNetCore.Identity;
using WebAppWithAuthenticationApi.Enums;

namespace WebAppWithAuthenticationApi.Models;

public class User : IdentityUser<int>
{
    public User(int id, string name, string surname, string? position, DateTime? lastLoginDate, UserStatus status,
        string? refreshToken = null, DateTime? refreshTokenExpirationDate = null)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Position = position;
        LastLoginDate = lastLoginDate;
        Status = status;
        RefreshToken = refreshToken;
        RefreshTokenExpirationDate = refreshTokenExpirationDate;
    }

    public string Name { get; private set; }

    public string Surname { get; private set; }

    public string? Position { get; private set; }

    public DateTime? LastLoginDate { get; private set; }

    public UserStatus Status { get; private set; }

    public string? RefreshToken { get; private set; }

    public DateTime? RefreshTokenExpirationDate { get; private set; }

    public void SetStatus(UserStatus status)
    {
        Status = status;
    }

    public void SetLastLoginDate(DateTime? lastLoginDate)
    {
        LastLoginDate = lastLoginDate;
    }

    public void SetRefreshToken(string refreshToken, DateTime refreshTokenExpirationDate)
    {
        if (refreshTokenExpirationDate < DateTime.Now || string.IsNullOrEmpty(refreshToken))
        {
            throw new ArgumentException("Invalid refresh token credentials");
        }

        RefreshToken = refreshToken;
        RefreshTokenExpirationDate = refreshTokenExpirationDate;
    }
    
    
}