using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(
        UserManager<User> userManager
    )
    {
        _userManager = userManager;
    }

    public async Task<List<UserDto>> GetAll()
    {
        return await _userManager.Users
            .Select(x => new UserDto(x.Name, x.Surname, x.Email, x.Position, x.LastLoginDate, x.Status))
            .ToListAsync();
    }

    public async Task<List<UserDto>> GetFiltered(string? filter)
    {
        var usersQueryable = _userManager.Users;

        if (!string.IsNullOrEmpty(filter))
        {
            usersQueryable = usersQueryable.Where(x => x.Name.Contains(filter) || x.Surname.Contains(filter));
        }
        
        return await usersQueryable
            .Select(x => new UserDto(x.Name, x.Surname, x.Email, x.Position, x.LastLoginDate, x.Status))
            .ToListAsync();
    }
}