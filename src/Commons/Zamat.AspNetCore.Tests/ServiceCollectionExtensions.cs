using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Zamat.AspNetCore.Tests;
using Zamat.AspNetCore.Tests.FakeAuthentication;

namespace Zamat.AspNetCore.Tests;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds a fake authentication scheme to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="options">An optional action to configure the <see cref="FakeAuthenticationSchemeOptions"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddFakeAuthentication(
        this IServiceCollection services,
        Action<FakeAuthenticationSchemeOptions>? options = null
    )
    {
        services
            .AddAuthentication(FakeAuthenticationHandler.AuthenticationType)
            .AddScheme<FakeAuthenticationSchemeOptions, FakeAuthenticationHandler>(
                FakeAuthenticationHandler.AuthenticationType,
                options
            );

        services.AddSingleton<
            IPostConfigureOptions<AuthenticationOptions>,
            FakeAuthenticationOptions
        >();

        return services;
    }

    /// <summary>
    /// Configures the services to use an in-memory distributed cache.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection UseInMemoryCache(this IServiceCollection services)
    {
        services.MockSingleton<IDistributedCache>(
            (_) => new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()))
        );

        return services;
    }

    /// <summary>
    /// Configures the services to use an in-memory database for the specified <see cref="DbContext"/>.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection UseInMemoryDatabase<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        var databaseContext = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<TContext>)
        );

        if (databaseContext is not null)
        {
            services.Remove(databaseContext);
        }

        services.AddDbContext<TContext>(options =>
        {
            options.UseInMemoryDatabase(nameof(TContext));
            options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

        return services;
    }

    /// <summary>
    /// Replaces an existing service of type TService in the service collection with a new scoped service of type TImplementation.
    /// If the service does not exist, it simply adds a new scoped service.
    /// </summary>
    /// <typeparam name="TService">The type of the service to replace or add.</typeparam>
    /// <typeparam name="TImplementation">The type of the new service.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection MockScoped<TService, TImplementation>(
        this IServiceCollection services
    )
        where TService : class
        where TImplementation : class, TService
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddScoped<TService, TImplementation>();

        return services;
    }

    /// <summary>
    /// Replaces an existing service of type TService in the service collection with a new scoped service created by the provided factory method.
    /// If the service does not exist, it simply adds a new scoped service.
    /// </summary>
    /// <typeparam name="TService">The type of the service to replace or add.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="action">The factory method to create the new service.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection MockScoped<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> action
    )
        where TService : class
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddScoped(action);

        return services;
    }

    /// <summary>
    /// Replaces an existing service of type TService in the service collection with a new singleton service of type TImplementation.
    /// If the service does not exist, it simply adds a new singleton service.
    /// </summary>
    /// <typeparam name="TService">The type of the service to replace or add.</typeparam>
    /// <typeparam name="TImplementation">The type of the new service.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection MockSingleton<TService, TImplementation>(
        this IServiceCollection services
    )
        where TService : class
        where TImplementation : class, TService
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddSingleton<TService, TImplementation>();

        return services;
    }

    /// <summary>
    /// Replaces an existing service of type TService in the service collection with a new singleton service created by the provided factory method.
    /// If the service does not exist, it simply adds a new singleton service.
    /// </summary>
    /// <typeparam name="TService">The type of the service to replace or add.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="action">The factory method to create the new service.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection MockSingleton<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService> action
    )
        where TService : class
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));

        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddSingleton(action);

        return services;
    }
}
