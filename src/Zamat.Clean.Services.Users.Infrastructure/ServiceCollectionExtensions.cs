using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamat.AspNetCore.MassTransit.RabbitMQ;
using Zamat.Clean.Infrastructure;
using Zamat.Clean.Services.Users.Core.Domain.Factories;
using Zamat.Clean.Services.Users.Core.Interfaces;
using Zamat.Clean.Services.Users.Core.UnitOfWork;
using Zamat.Clean.Services.Users.Infrastructure.EFCore;
using Zamat.Clean.Services.Users.Infrastructure.Factories;
using Zamat.Clean.Services.Users.Infrastructure.Queries;
using Zamat.Common.EntityFrameworkCore;

namespace Zamat.Clean.Services.Users.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEventBus();

        services.AddUsersDbContext(configuration);
        services.ConfigureMessageBroker(configuration);

        services.AddSingleton<IUserFactory, UserFactory>();

        services.AddScoped<IUsersQueries, UsersQueries>();

        services.AddScoped<IApplicationUnitOfWork, UnitOfWork.UnitOfWork>();

        return services;
    }

    private static IServiceCollection ConfigureMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitConnectionString = configuration.GetConnectionString("RabbitMQ") ?? throw new InvalidOperationException("Connection string for rabbitMQ not set.");

        var opt = new RabbitMQOptions()
        {
            Host = rabbitConnectionString,
            Prefix = "zamat-users-svc"
        };

        services.ConfigureMassTransitWithOutbox<UsersDbContext>(opt, _ => { }, _ => { });

        services.AddHealthChecks().AddRabbitMQ(rabbitConnectionString: rabbitConnectionString);

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
                migrationCtx.Schema = Consts.DefaultDatabaseSchema;
            });
        });

        services.AddHealthChecks().AddDbContextCheck<UsersDbContext>(nameof(UsersDbContext));

        return services;
    }
}