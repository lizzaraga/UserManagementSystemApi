using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Api.Models;

public class RegisterReqDto
{
    [Required] public required string Email { get; set; }
    [Required] public required string Password { get; set; }
    [Required] public required string ConfirmPassword { get; set; }
}