using UserManagementSystem.Api.Models;

namespace UserManagementSystem.Api.Services.Ifs;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAll();
    Task<UserDto?> GetById(string id);
    Task<string?> DeleteById(string id);
    
}