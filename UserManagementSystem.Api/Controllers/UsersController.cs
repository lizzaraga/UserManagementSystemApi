using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Api.Models;
using UserManagementSystem.Api.Services.Ifs;
using UserManagementSystem.Database;
using UserManagementSystem.Database.Entities;

namespace UserManagementSystem.Api.Controllers;


[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class UsersController(
    IUserService userService
    ): ControllerBase
{
    [Authorize(Roles = AppRoles.Administrator)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var result = await userService.GetAll();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<User>>> GetById(string id)
    {
        var result = await userService.GetById(id);
        if (result is null)
        {
            ModelState.AddModelError("error", "User not found !");
            return BadRequest(ModelState);
        }
        return Ok(result);
    }
    
    [Authorize(Roles = AppRoles.Administrator)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteById(string id)
    {
        var result = await userService.DeleteById(id);
        if (result is null)
        {
            ModelState.AddModelError("error", "User not found !");
            return BadRequest(ModelState);
        }
        return Ok(id);
    }
}