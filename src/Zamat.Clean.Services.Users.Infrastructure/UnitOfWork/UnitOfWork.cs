﻿using Zamat.BuildingBlocks.Domain;
using Zamat.Clean.Services.Users.Core.Domain.Repositories;
using Zamat.Clean.Services.Users.Core.UnitOfWork;
using Zamat.Clean.Services.Users.Infrastructure.EFCore;
using Zamat.Clean.Services.Users.Infrastructure.Repositories;

namespace Zamat.Clean.Services.Users.Infrastructure.UnitOfWork;

internal class UnitOfWork(UsersDbContext dbContext, IDomainEventDispatcher domainEventDispatcher) : IApplicationUnitOfWork
{
    private readonly IUserRepository _userRepository = new UserRepository(dbContext);
    private readonly IDomainEventDispatcher _domainEventDispatcher = domainEventDispatcher;
    private readonly UsersDbContext _dbContext = dbContext;

    public IUserRepository UserRepository => _userRepository;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(cancellationToken);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task DispatchDomainEvents(CancellationToken cancellationToken)
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