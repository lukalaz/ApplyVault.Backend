using ApplyVault.Domain.Enums;

namespace ApplyVault.Domain.Entities;

public class JobApplication
{
    public Guid Id { get; private set; } = Guid.NewGuid();

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

    // EF Core needs a parameterless constructor
    private JobApplication() { }

    public JobApplication(
        string company,
        string role,
        string? location = null,
        bool isRemote = false,
        string? referral = null,
        string? contactPerson = null,
        DateTime? dateApplied = null,
        string? compensationRange = null,
        DateTime? lastTouch = null,
        string? nextAction = null,
        DateTime? nextActionDate = null,
        string? notes = null,
        string? link = null
    )
    {
        SetCompany(company);
        SetRole(role);

        Location = location?.Trim() ?? string.Empty;
        IsRemote = isRemote;

        Referral = referral?.Trim() ?? string.Empty;
        ContactPerson = contactPerson?.Trim() ?? string.Empty;

        DateApplied = dateApplied;

        CompensationRange = compensationRange?.Trim() ?? string.Empty;

        LastTouch = lastTouch;

        NextAction = nextAction?.Trim() ?? string.Empty;
        NextActionDate = nextActionDate;

        Notes = notes?.Trim() ?? string.Empty;
        Link = link?.Trim() ?? string.Empty;
    }

    public void UpdateStatus(ApplicationStatus status) => Status = status;

    public void UpdateLastTouch(DateTime? lastTouch) => LastTouch = lastTouch;

    public void UpdateNotes(string? notes) => Notes = notes?.Trim() ?? string.Empty;

    private void SetCompany(string company)
    {
        if (string.IsNullOrWhiteSpace(company))
            throw new ArgumentException("Company is required.", nameof(company));

        Company = company.Trim();
    }

    private void SetRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role is required.", nameof(role));

        Role = role.Trim();
    }
}
