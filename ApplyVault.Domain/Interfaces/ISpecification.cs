using ApplyVault.Domain.Shared.ComplexTypes;
using System.Linq.Expressions;

namespace ParkingSpaceManagement.Core.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Pagination Pagination { get; }
    }
}
