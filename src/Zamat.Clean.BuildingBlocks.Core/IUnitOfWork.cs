using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zamat.Clean.BuildingBlocks.Core;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default!);
}
