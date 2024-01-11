namespace Zamat.BuildingBlocks.Core;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default!);
}
