using Microsoft.EntityFrameworkCore;
using Practica04.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Practica04.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;

namespace Practica04.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>Provides the database session for the Stripe practice application.</summary>
/// <param name="options">The database provider and connection options.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        builder.UseSnakeCaseNamingConvention();
    }
}


