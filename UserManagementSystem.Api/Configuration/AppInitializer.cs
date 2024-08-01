using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Database;
using UserManagementSystem.Database.Entities;

namespace UserManagementSystem.Api.Configuration;

public static class AppInitializer
{
    public static async Task InitializeDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetService<UmsDbContext>();
            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
            await SetupAdministrator(app, dbContext!, userManager!);
        }
    }

    private static async Task SetupAdministrator(WebApplication app, UmsDbContext dbContext, UserManager<User> userManager)
    {
        var administrators = await userManager.GetUsersForClaimAsync(new Claim(ClaimTypes.Role, AppRoles.Administrator));
        if (administrators.Count == 0)
        {
            using (var transaction = await dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var roleClaim = new Claim(ClaimTypes.Role, AppRoles.Administrator);
                    var user = new User()
                    {
                        UserName = app.Configuration["Admin:Email"],
                        Email = app.Configuration["Admin:Email"],
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(user, app.Configuration["Admin:Password"]!);
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
}