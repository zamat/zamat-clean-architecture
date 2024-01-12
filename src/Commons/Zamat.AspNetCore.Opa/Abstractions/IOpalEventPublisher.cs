using System.Threading;
using System.Threading.Tasks;

namespace AUMS.AspNetCore.Opa.Abstractions;

public interface IOpalEventPublisher
{
    Task PublishAsync(DataSourceUpdateEvent dataSourceUpdateEvent, CancellationToken cancellationToken);
}
