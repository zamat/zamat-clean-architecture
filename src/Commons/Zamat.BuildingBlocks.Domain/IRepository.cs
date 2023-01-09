using System.Threading;
using System.Threading.Tasks;
using Zamat.BuildingBlocks.Domain.Specifications;

namespace Zamat.BuildingBlocks.Domain;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task<bool> CheckExistsAsync(Specification<T> specification, CancellationToken cancellationToken);
    Task<T> GetAsync(Specification<T> specification, CancellationToken cancellationToken);
    Task<T?> GetOrDefaultAsync(Specification<T> specification, CancellationToken cancellationToken);
    Task AddAsync(T user, CancellationToken cancellationToken);
    Task DeleteAsync(T user, CancellationToken cancellationToken);
}
