using Application.Common.Interfaces;
using Domain.Common;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>, IApplicationDbContext
{
    private readonly IUser _currentUser;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IUser currentUser)
        : base(options)
    {
        _currentUser = currentUser;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Created = DateTime.UtcNow;
                entry.Entity.CreatedBy = _currentUser.Id;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.LastModified = DateTime.UtcNow;
                entry.Entity.LastModifiedBy = _currentUser.Id;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}