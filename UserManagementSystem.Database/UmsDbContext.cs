using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Database.Entities;
using UserManagementSystem.Database.Interfaces;

namespace UserManagementSystem.Database;

public class UmsDbContext(DbContextOptions<UmsDbContext> options): IdentityDbContext<User>(options)
{
    public DbSet<UserProfile> UserProfiles { get; set; }


    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        AutoSetDateTrackedEntities();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        AutoSetDateTrackedEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        AutoSetDateTrackedEntities();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override int SaveChanges()
    {
        AutoSetDateTrackedEntities();
        return base.SaveChanges();
    }

    private void AutoSetDateTrackedEntities()
    {
        ChangeTracker.Entries<IDateTrackedEntity>()
            .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified)
            .ToList()
            .ForEach(entry =>
            {
                var now = DateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdateAt = now;
                }
                entry.Entity.UpdateAt = now;
            });
    }
}