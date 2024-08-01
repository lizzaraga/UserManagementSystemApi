using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserManagementSystem.Api.Configuration;
using UserManagementSystem.Api.Models;
using UserManagementSystem.Api.Services.Ifs;
using UserManagementSystem.Database;
using UserManagementSystem.Database.Entities;

namespace UserManagementSystem.Api.Services;

public class AuthService(
    IOptions<JwtConfig> jwtOptions,
    UserManager<User> userManager,
    UmsDbContext dbContext
    ): IAuthService
{
    public async Task<UserToken?> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null) return null;
        var isCorrectPwd = await userManager.CheckPasswordAsync(user, password);
        if (!isCorrectPwd) return null;
        var claims = await userManager.GetClaimsAsync(user);
        var expiresAt = DateTime.UtcNow.AddDays(2);
        var securityToken = new JwtSecurityToken(
            issuer: jwtOptions.Value.Issuer,
            audience: jwtOptions.Value.Audience,
            expires: expiresAt,
            claims: claims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.Secret)), SecurityAlgorithms.HmacSha256)
            
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return new UserToken()
        {
            Token = tokenHandler.WriteToken(securityToken),
            ExpiresAt = expiresAt
        };
    }

    public async Task Register(RegisterReqDto dto)
    {

        using (var transaction = await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var roleClaim = new Claim(ClaimTypes.Role, AppRoles.BasicUser);
                var user = new User()
                {
                    Email = dto.Email,
                    UserName = dto.Email,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, dto.Password);
                await userManager.AddClaimAsync(user, claim: roleClaim);
                
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}