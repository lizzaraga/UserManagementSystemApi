using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Api.Models;
using UserManagementSystem.Api.Services.Ifs;
using UserManagementSystem.Database.Entities;

namespace UserManagementSystem.Api.Services;

public class AuthService(
    UserManager<User> userManager
    ): IAuthService
{
    public async Task<bool> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null) return false;
        var isCorrectPwd = await userManager.CheckPasswordAsync(user, password);
        return isCorrectPwd;
    }

    public async Task Register(RegisterReqDto dto)
    {
        var user = new User()
        {
            Email = dto.Email,
            UserName = dto.Email,
            EmailConfirmed = true
        };
        await userManager.CreateAsync(user, dto.Password);
       

    }
}