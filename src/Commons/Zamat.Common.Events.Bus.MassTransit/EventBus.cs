﻿using System.Threading;
using System.Threading.Tasks;
using MassTransit;

namespace Zamat.Common.Events.Bus.MassTransit;

class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent
    {
        return _publishEndpoint.Publish(@event, cancellationToken);
    }
}
