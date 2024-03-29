﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamat.AspNetCore.MassTransit.RabbitMQ;
using Zamat.Common.EntityFrameworkCore;
using Zamat.Sample.BuildingBlocks.Infrastructure;
using Zamat.Sample.Services.Users.Core.Domain.Factories;
using Zamat.Sample.Services.Users.Core.Interfaces;
using Zamat.Sample.Services.Users.Core.UnitOfWork;
using Zamat.Sample.Services.Users.Infrastructure.EFCore;
using Zamat.Sample.Services.Users.Infrastructure.Factories;
using Zamat.Sample.Services.Users.Infrastructure.Queries;

namespace Zamat.Sample.Services.Users.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventBus();

        services.AddUsersDbContext(configuration);
        services.ConfigureMessageBroker(configuration);

        services.AddSingleton<IUserFactory, UserFactory>();

        services.AddScoped<IUsersQueries, UsersQueries>();

        services.AddScoped<IApplicationUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitConnectionString = configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Connection string for rabbitMQ not set.");

        var opt = new RabbitMQOptions()
        {
            Host = rabbitConnectionString,
            Prefix = "users-svc"
        };

        services.ConfigureMassTransitWithOutbox<UsersDbContext>(opt, _ => { }, _ => { });

        return services;
    }

    public static IServiceCollection AddUsersDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString(nameof(UsersDbContext)) ?? throw new InvalidOperationException("Connection string for db not set.");

        services.AddDbContext<UsersDbContext>(options =>
        {
            options.UseAutoDbProvider(dbConnectionString, migrationCtx =>
            {
                migrationCtx.PostgreSQL = "EFCore.PostgreSQL";
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitConnectionString = configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Connection string for rabbitMQ not set.");

        services.AddHealthChecks()
            .AddDbContextCheck<UsersDbContext>(nameof(UsersDbContext))
            .AddRabbitMQ(rabbitConnectionString: rabbitConnectionString);

        return services;
    }
}