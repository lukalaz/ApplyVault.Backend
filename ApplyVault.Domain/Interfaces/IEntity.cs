namespace ApplyVault.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTimeOffset CreatedAt { get; }
        DateTimeOffset UpdatedAt { get; }
    }
}
