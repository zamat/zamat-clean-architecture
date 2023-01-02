using System.Collections.Generic;

namespace Zamat.BuildingBlocks.Domain;

public abstract class Entity<TIdentifier> : IEntity where TIdentifier : notnull
{
    public virtual TIdentifier Id { get; protected set; } = default!;

    private List<IDomainEvent>? _domainEvents;

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents ?? new List<IDomainEvent>(capacity: 0);

    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem) => _domainEvents?.Remove(eventItem);

    public void ClearDomainEvents() => _domainEvents?.Clear();

    protected bool Equals(Entity<TIdentifier> other)
    {
        return EqualityComparer<TIdentifier>.Default.Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TIdentifier> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (Id.Equals(default(TIdentifier)) || other.Id.Equals(default(TIdentifier)))
            return false;

        return Id.Equals(other.Id);
    }

    public static bool operator ==(Entity<TIdentifier> a, Entity<TIdentifier> b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Entity<TIdentifier> a, Entity<TIdentifier> b)
        => !(a == b);

    public override int GetHashCode()
        => (GetType() + Id.ToString()).GetHashCode();
}
