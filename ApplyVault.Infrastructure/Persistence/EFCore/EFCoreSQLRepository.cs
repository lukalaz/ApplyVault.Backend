using ApplyVault.Application.Interfaces;
using ApplyVault.Domain.Interfaces;
using ApplyVault.Infrastructure.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using ParkingSpaceManagement.Core.Interfaces;

namespace ApplyVault.Infrastructure.Data.EFCore;

public class EFCoreSQLRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly ApplicationDbContext _context;

    public EFCoreSQLRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Add(T entity, CancellationToken token = default)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        await _context.AddAsync(entity, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task Delete(T entity, CancellationToken token = default)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        _context.Remove(entity);
        await _context.SaveChangesAsync(token);
    }

    public async Task Edit(T entity, CancellationToken token = default)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        _context.Update(entity);
        await _context.SaveChangesAsync(token);
    }

    public async Task<T?> Get(Guid id, CancellationToken token = default)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(x => x.Id == id, token);
    }

    public async Task<IReadOnlyCollection<T>> List(ISpecification<T> spec, CancellationToken token = default)
    {
        var queryableResultWithIncludes = spec.Includes
            .Aggregate(_context.Set<T>().AsQueryable(),
                (current, include) => current.Include(include));

        var queryWithAllIncludes = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes,
                (current, include) => current.Include(include));

        var filteredQuery = queryWithAllIncludes.Where(spec.Criteria);

        if (spec.Pagination is { PageNumber: > 0, PageSize: > 0 })
        {
            var skip = (spec.Pagination.PageNumber - 1) * spec.Pagination.PageSize;
            filteredQuery = filteredQuery.Skip(skip).Take(spec.Pagination.PageSize);
        }

        return await filteredQuery.ToListAsync(token);
    }
}