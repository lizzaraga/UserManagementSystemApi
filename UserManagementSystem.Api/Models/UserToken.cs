namespace UserManagementSystem.Api.Models;

public class UserToken
{
    public required string Token { get; set; }
    public required DateTime ExpiresAt { get; set; }
}