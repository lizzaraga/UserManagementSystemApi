using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Api.Models;

public class LoginReqDto
{
    [Required] public required string Email { get; set; }
    [Required] public required string Password { get; set; }
}