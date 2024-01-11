﻿using MassTransit;

namespace Zamat.Common.Events.Bus.MassTransit;

class EventBus(IPublishEndpoint publishEndpoint) : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
    {
        return _publishEndpoint.Publish(@event, cancellationToken);
    }
}
