using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Database.Interfaces;

namespace UserManagementSystem.Database.Entities;

public class User: IdentityUser, IDateTrackedEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}