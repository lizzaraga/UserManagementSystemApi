using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Api.Models;
using UserManagementSystem.Api.Services.Ifs;

namespace UserManagementSystem.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class AuthController(
    IAuthService authService
    ): ControllerBase
{
    
    [HttpPost("Login")]
    public async Task<ActionResult<bool>> Login(LoginReqDto dto)
    {
        var result = await authService.Login(dto.Email, dto.Password);
        return Ok(result);
    }
    
    [HttpPost("Register")]
    public async Task<ActionResult<bool>> Register(RegisterReqDto dto)
    {
        await authService.Register(dto);
        return Ok();
    }
}