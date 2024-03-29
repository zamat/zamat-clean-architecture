﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zamat.Common.EntityFrameworkCore;
using Zamat.Sample.Services.Content.Core.Interfaces;
using Zamat.Sample.Services.Content.Infrastructure.EFCore;
using Zamat.Sample.Services.Content.Infrastructure.Repositories;

namespace Zamat.Sample.Services.Content.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddContentDbContext(configuration);

        services.AddScoped<IArticleRepository, ArticleRepository>();

        return services;
    }

    public static IServiceCollection AddContentDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString(nameof(ContentDbContext)) ?? throw new InvalidOperationException("Connection string for db not set.");

        services.AddDbContext<ContentDbContext>(options =>
        {
            options.UseAutoDbProvider(dbConnectionString, migrationCtx =>
            {
                migrationCtx.PostgreSQL = "EFCore.PostgreSQL";
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<ContentDbContext>(nameof(ContentDbContext));

        return services;
    }
}
