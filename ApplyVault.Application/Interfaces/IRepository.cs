using ApplyVault.Domain.Interfaces;
using ParkingSpaceManagement.Core.Interfaces;

namespace ApplyVault.Application.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<T?> Get(Guid id, CancellationToken token = default);
        Task<IReadOnlyCollection<T>> List(ISpecification<T> spec, CancellationToken token = default);
        Task Add(T entity, CancellationToken token = default);
        Task Delete(T entity, CancellationToken token = default);
        Task Edit(T entity, CancellationToken token = default);
    }
}