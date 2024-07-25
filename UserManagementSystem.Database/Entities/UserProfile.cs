using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Database.Interfaces;

namespace UserManagementSystem.Database.Entities;

public class UserProfile: IDateTrackedEntity
{
    public Guid id { get; set; }
    
    [Required] [MaxLength(30)] public required string FirstName { get; set; }
    [Required] [MaxLength(30)] public required string LastName { get; set; }
    [Required] public required int Age { get; set; } = default!;

    [ForeignKey("User")] [MaxLength(100)] public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}