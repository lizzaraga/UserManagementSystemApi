using UserManagementSystem.Database.Interfaces;

namespace UserManagementSystem.Api.Models;

public class UserDto: IDateTrackedEntity
{
    public required string Id { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required bool EmailConfirmed { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdateAt { get; set; }
}