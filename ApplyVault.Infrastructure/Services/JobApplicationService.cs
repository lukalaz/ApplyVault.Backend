using ApplyVault.Application.Commands.JobApplications;
using ApplyVault.Application.Dtos.JobApplications;
using ApplyVault.Application.Interfaces;
using ApplyVault.Domain.Entities;
using ApplyVault.Infrastructure.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApplyVault.Infrastructure.Services;

public sealed class JobApplicationService : IJobApplicationService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<JobApplicationService> _logger;

    public JobApplicationService(ApplicationDbContext db, ILogger<JobApplicationService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<IReadOnlyList<JobApplicationResponseDto>> GetAllAsync(CancellationToken ct)
    {
        var items = await _db.JobApplications
            .AsNoTracking()
            .OrderByDescending(x => x.DateApplied ?? DateTime.MinValue)
            .ThenByDescending(x => x.LastTouch ?? DateTime.MinValue)
            .ToListAsync(ct);

        return items.Select(ToDto).ToList();
    }

    public async Task<JobApplicationResponseDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.JobApplications
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        return entity is null ? null : ToDto(entity);
    }

    public async Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationCommand command, CancellationToken ct)
    {
        var entity = JobApplication.Create(
            command.Company,
            command.Role,
            command.Location,
            command.IsRemote,
            command.Referral,
            command.ContactPerson,
            command.DateApplied,
            command.Status,
            command.CompensationRange,
            command.LastTouch,
            command.NextAction,
            command.NextActionDate,
            command.Notes,
            command.Link
        );

        _db.JobApplications.Add(entity);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Created JobApplication {Id} ({Company} - {Role})", entity.Id, entity.Company, entity.Role);

        return ToDto(entity);
    }

    public async Task<JobApplicationResponseDto?> UpdateAsync(UpdateJobApplicationCommand command, CancellationToken ct)
    {
        var entity = await _db.JobApplications.FirstOrDefaultAsync(x => x.Id == command.Id, ct);
        if (entity is null)
            return null;

        entity.Update(
            command.Company,
            command.Role,
            command.Location,
            command.IsRemote,
            command.Referral,
            command.ContactPerson,
            command.DateApplied,
            command.Status,
            command.CompensationRange,
            command.LastTouch,
            command.NextAction,
            command.NextActionDate,
            command.Notes,
            command.Link
        );

        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Updated JobApplication {Id}", entity.Id);

        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _db.JobApplications.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null)
            return false;

        _db.JobApplications.Remove(entity);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Deleted JobApplication {Id}", id);

        return true;
    }

    private static JobApplicationResponseDto ToDto(JobApplication e) =>
        new(
            e.Id,
            e.Company,
            e.Role,
            e.Location,
            e.IsRemote,
            e.Referral,
            e.ContactPerson,
            e.DateApplied,
            e.Status,
            e.CompensationRange,
            e.LastTouch,
            e.NextAction,
            e.NextActionDate,
            e.Notes,
            e.Link
        );
}
