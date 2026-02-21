using ApplyVault.Domain.Enums;

namespace ApplyVault.Application.Commands.JobApplications;

public sealed record CreateJobApplicationCommand(
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