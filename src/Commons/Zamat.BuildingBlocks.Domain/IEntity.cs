using System.Collections.Generic;

namespace Zamat.BuildingBlocks.Domain;

public interface IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void RemoveDomainEvent(IDomainEvent eventItem);
    void ClearDomainEvents();
}
