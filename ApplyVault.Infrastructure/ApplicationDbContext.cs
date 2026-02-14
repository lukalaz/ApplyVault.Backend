using Microsoft.EntityFrameworkCore;
using ApplyVault.Domain;

namespace ApplyVault.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<JobApplication> JobApplications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<JobApplication>(eb =>
        {
            eb.HasKey(e => e.Id);
            eb.Property(e => e.Company).HasMaxLength(200);
            eb.Property(e => e.Role).HasMaxLength(200);
            eb.Property(e => e.Location).HasMaxLength(200);
            eb.Property(e => e.Referral).HasMaxLength(200);
            eb.Property(e => e.ContactPerson).HasMaxLength(200);
            eb.Property(e => e.CompensationRange).HasMaxLength(100);
            eb.Property(e => e.NextAction).HasMaxLength(500);
            eb.Property(e => e.Notes).HasColumnType("TEXT");
            eb.Property(e => e.Link).HasMaxLength(1000);
        });
    }
}
