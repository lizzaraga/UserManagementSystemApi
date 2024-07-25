using UserManagementSystem.Api.Models;
using UserManagementSystem.Database.Entities;

namespace UserManagementSystem.Api.Services.Ifs;

public interface IAuthService
{
    Task<bool> Login(string email, string password);
    Task Register(RegisterReqDto dto);
}