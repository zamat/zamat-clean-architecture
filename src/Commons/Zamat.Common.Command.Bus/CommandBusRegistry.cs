﻿using System;

namespace Zamat.Common.Command.Bus;

class CommandBusRegistry : ICommandBusRegistry
{
    private readonly IServiceProvider _serviceProvider;

    public CommandBusRegistry(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICommandHandler<TCommand> GetCommandHandler<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

        var handler = _serviceProvider.GetService(handlerType);

        if (handler is not ICommandHandler<TCommand> commandHandler)
        {
            throw new CommandBusException($"Command handler for {command.GetType().FullName} not found.");
        }

        return commandHandler;
    }
}
