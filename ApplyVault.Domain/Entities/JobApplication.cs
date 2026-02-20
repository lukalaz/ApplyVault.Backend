using ApplyVault.Domain.Enums;

namespace ApplyVault.Domain.Entities;

public class JobApplication
{
    // EF Core needs a parameterless constructor; private is fine for EF,
    // but it makes `new JobApplication { ... }` impossible from outside.
    private JobApplication() { }

    public Guid Id { get; private set; }

    public string Company { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;

    public string Location { get; private set; } = string.Empty;
    public bool IsRemote { get; private set; }

    public string Referral { get; private set; } = string.Empty;
    public string ContactPerson { get; private set; } = string.Empty;

    public DateTime? DateApplied { get; private set; }

    public ApplicationStatus Status { get; private set; } = ApplicationStatus.Planned;

    public string CompensationRange { get; private set; } = string.Empty;

    public DateTime? LastTouch { get; private set; }

    public string NextAction { get; private set; } = string.Empty;
    public DateTime? NextActionDate { get; private set; }

    public string Notes { get; private set; } = string.Empty;

    public string Link { get; private set; } = string.Empty;

    public static JobApplication Create(
        string company,
        string role,
        string? location,
        bool isRemote,
        string? referral,
        string? contactPerson,
        DateTime? dateApplied,
        ApplicationStatus status,
        string? compensationRange,
        DateTime? lastTouch,
        string? nextAction,
        DateTime? nextActionDate,
        string? notes,
        string? link)
    {
        if (string.IsNullOrWhiteSpace(company))
            throw new ArgumentException("Company is required.", nameof(company));

        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role is required.", nameof(role));

        var entity = new JobApplication
        {
            Id = Guid.NewGuid()
        };

        entity.Update(
            company,
            role,
            location,
            isRemote,
            referral,
            contactPerson,
            dateApplied,
            status,
            compensationRange,
            lastTouch,
            nextAction,
            nextActionDate,
            notes,
            link
        );

        return entity;
    }

    public void Update(
        string company,
        string role,
        string? location,
        bool isRemote,
        string? referral,
        string? contactPerson,
        DateTime? dateApplied,
        ApplicationStatus status,
        string? compensationRange,
        DateTime? lastTouch,
        string? nextAction,
        DateTime? nextActionDate,
        string? notes,
        string? link)
    {
        if (string.IsNullOrWhiteSpace(company))
            throw new ArgumentException("Company is required.", nameof(company));

        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role is required.", nameof(role));

        Company = company.Trim();
        Role = role.Trim();

        Location = location?.Trim() ?? string.Empty;
        IsRemote = isRemote;

        Referral = referral?.Trim() ?? string.Empty;
        ContactPerson = contactPerson?.Trim() ?? string.Empty;

        DateApplied = dateApplied;
        Status = status;

        CompensationRange = compensationRange?.Trim() ?? string.Empty;

        LastTouch = lastTouch;

        NextAction = nextAction?.Trim() ?? string.Empty;
        NextActionDate = nextActionDate;

        Notes = notes?.Trim() ?? string.Empty;
        Link = link?.Trim() ?? string.Empty;
    }
}
