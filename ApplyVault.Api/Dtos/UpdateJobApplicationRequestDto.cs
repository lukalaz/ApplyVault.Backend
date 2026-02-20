using ApplyVault.Domain.Enums;

namespace ApplyVault.Api.Dtos.JobApplications;

public record UpdateJobApplicationRequestDto(
    string Company,
    string Role,
    string? Location,
    bool IsRemote,
    string? Referral,
    string? ContactPerson,
    DateTime? DateApplied,
    ApplicationStatus Status,
    string? CompensationRange,
    DateTime? LastTouch,
    string? NextAction,
    DateTime? NextActionDate,
    string? Notes,
    string? Link
);
