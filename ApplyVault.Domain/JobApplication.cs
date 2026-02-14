namespace ApplyVault.Domain;

public class JobApplication
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Company { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    // Location / Remote
    public string Location { get; set; } = string.Empty;
    public bool IsRemote { get; set; }

    // Referral / Introduced by
    public string Referral { get; set; } = string.Empty;

    public string ContactPerson { get; set; } = string.Empty;

    public DateTime? DateApplied { get; set; }

    public ApplicationStatus Status { get; set; } = ApplicationStatus.Planned;

    // Compensation range / target (e.g. "70k-90k", or "100000")
    public string CompensationRange { get; set; } = string.Empty;

    public DateTime? LastTouch { get; set; }

    public string NextAction { get; set; } = string.Empty;

    public DateTime? NextActionDate { get; set; }

    public string Notes { get; set; } = string.Empty;

    public string Link { get; set; } = string.Empty;
}

// status enum moved to ApplicationStatus.cs
