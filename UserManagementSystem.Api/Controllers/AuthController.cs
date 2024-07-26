using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Api.Models;
using UserManagementSystem.Api.Services.Ifs;
using UserManagementSystem.Database.Entities;

namespace UserManagementSystem.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class AuthController(
    IAuthService authService,
    UserManager<User> userManager): ControllerBase
{
    
    [HttpPost("Login")]
    public async Task<ActionResult<UserToken>> Login(LoginReqDto dto)
    {
        var result = await authService.Login(dto.Email, dto.Password);
        if (result is null)
        {
            ModelState.AddModelError("error", "Bad credentials !");
            return Unauthorized(ModelState);
        }
        return Ok(result);
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult<bool>> Register(RegisterReqDto dto)
    {
        await authService.Register(dto);
        return Ok();
    }

    [Authorize]
    [HttpGet("AllUsers")]
    public IActionResult GetAllUsers()
    {
        return Ok(userManager.Users.ToList());
    }
}