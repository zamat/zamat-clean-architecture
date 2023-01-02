using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zamat.BuildingBlocks.Domain;
using Zamat.Sample.Services.Users.Core.Domain.Repositories;
using Zamat.Sample.Services.Users.Core.UnitOfWork;
using Zamat.Sample.Services.Users.Infrastructure.EFCore;
using Zamat.Sample.Services.Users.Infrastructure.Repositories;

namespace Zamat.Sample.Services.Users.Infrastructure;

class UnitOfWork : IApplicationUnitOfWork
{
    private readonly IUserRepository _userRepository;
    private readonly IDomainEventDispatcher _domainEventDispatcher;
    private readonly UsersDbContext _dbContext;

    public UnitOfWork(UsersDbContext dbContext, IDomainEventDispatcher domainEventDispatcher)
    {
        _userRepository = new UserRepository(dbContext);
        _domainEventDispatcher = domainEventDispatcher;
        _dbContext = dbContext;
    }

    public IUserRepository UserRepository => _userRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(cancellationToken);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    async Task DispatchDomainEvents(CancellationToken cancellationToken)
    {
        var entities = _dbContext.ChangeTracker
            .Entries<IEntity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        entities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _domainEventDispatcher.DispatchAsync(domainEvent, cancellationToken);
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
