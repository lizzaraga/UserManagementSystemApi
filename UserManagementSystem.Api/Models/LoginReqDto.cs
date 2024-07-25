using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Api.Models;

public class LoginReqDto
{
    [Required] [EmailAddress] public required string Email { get; set; }
    [Required] [DataType(DataType.Password)] public required string Password { get; set; }
}