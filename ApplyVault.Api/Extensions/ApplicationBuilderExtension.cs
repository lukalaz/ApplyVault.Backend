using ApplyVault.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApplyVault.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var logger = app.Logger;

        logger.LogInformation("Applying EF Core migrations...");
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
        logger.LogInformation("Database migrated successfully.");
    }
}
