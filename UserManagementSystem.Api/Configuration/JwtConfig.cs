namespace UserManagementSystem.Api.Configuration;

public class JwtConfig
{
    public required string Audience { get; set; }
    public required string Issuer { get; set; }
    public required string Secret { get; set; }
}