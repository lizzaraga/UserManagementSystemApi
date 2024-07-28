using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Api.Models;
using UserManagementSystem.Api.Services.Ifs;
using UserManagementSystem.Database.Entities;

namespace UserManagementSystem.Api.Services;

public class UserService(
    UserManager<User> userManager,
    IMapper mapper
    ): IUserService
{
    public Task<IEnumerable<UserDto>> GetAll()
    {
        var result = userManager.Users.ToList().AsEnumerable();
        return Task.FromResult(mapper.Map<IEnumerable<UserDto>>(result));
    }

    public Task<UserDto?> GetById(string id)
    {
        var result = userManager.Users.FirstOrDefault(u => u.Id.Equals(id));
        return Task.FromResult(mapper.Map<UserDto?>(result));
    }

    public async Task<string?> DeleteById(string id)
    {
        var result = userManager.Users.FirstOrDefault(u => u.Id.Equals(id));
        if (result is not null) await userManager.DeleteAsync(result);
        return result?.Id;
    }
}