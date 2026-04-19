using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppWithAuthenticationApi.Data;
using WebAppWithAuthenticationApi.Dtos;
using WebAppWithAuthenticationApi.Enums;
using WebAppWithAuthenticationApi.Models;

namespace WebAppWithAuthenticationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _dbContext;

    public UserController(
        UserManager<User> userManager,
        AppDbContext dbContext
    )
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<UserDto>> GetList(string? filter)
    {
        var usersQueryable = _userManager.Users;

        if (!string.IsNullOrEmpty(filter))
        {
            usersQueryable = usersQueryable.Where(x => x.Name.Contains(filter) || x.Surname.Contains(filter));
        }
        
        return await usersQueryable
            .Select(x => new UserDto(x.Id, x.Name, x.Surname, x.Email, x.Position, x.LastLoginDate, x.Status))
            .ToListAsync();
    }

    [HttpPost("block")]
    public async Task<ActionResult> Block([FromBody] List<int> idList)
    {
        var selectedUsers = await _userManager.Users.Where(x => idList.Contains(x.Id)).ToListAsync();

        try
        {
            selectedUsers.ForEach(x => x.SetStatus(UserStatus.Blocked));
            _dbContext.Users.UpdateRange(selectedUsers);
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            return StatusCode(500, "Internal server error");
        }
        
        return Ok();
    }
}