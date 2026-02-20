using ApplyVault.Domain.Entities;
using ApplyVault.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ApplyVault.Infrastructure.Persistence.EFCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<JobApplication> JobApplications => Set<JobApplication>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new JobApplicationConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
