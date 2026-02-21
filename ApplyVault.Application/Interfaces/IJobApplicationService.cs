using ApplyVault.Application.Commands.JobApplications;
using ApplyVault.Application.Dtos.JobApplications;

namespace ApplyVault.Application.Interfaces;

public interface IJobApplicationService
{
    Task<IReadOnlyList<JobApplicationResponseDto>> GetAllAsync(CancellationToken ct);
    Task<JobApplicationResponseDto?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationCommand command, CancellationToken ct);
    Task<JobApplicationResponseDto?> UpdateAsync(UpdateJobApplicationCommand command, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}