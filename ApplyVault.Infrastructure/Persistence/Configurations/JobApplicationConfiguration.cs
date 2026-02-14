using ApplyVault.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplyVault.Infrastructure.Persistence.Configurations;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> eb)
    {
        eb.HasKey(e => e.Id);

        eb.Property(e => e.Company).HasMaxLength(200).IsRequired();
        eb.Property(e => e.Role).HasMaxLength(200).IsRequired();

        eb.Property(e => e.Location).HasMaxLength(200);
        eb.Property(e => e.Referral).HasMaxLength(200);
        eb.Property(e => e.ContactPerson).HasMaxLength(200);

        eb.Property(e => e.CompensationRange).HasMaxLength(100);

        eb.Property(e => e.NextAction).HasMaxLength(500);

        eb.Property(e => e.Notes).HasColumnType("TEXT");

        eb.Property(e => e.Link).HasMaxLength(1000);

        // Enums: stored as int by default; explicit is fine too
        eb.Property(e => e.Status).IsRequired();
    }
}
