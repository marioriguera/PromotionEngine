using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.Application.Features.Promotions.GetAll.V1;
using PromotionEngine.Application.Features.Promotions.GetAll.V1.Mappers;
using PromotionEngine.Application.Features.Promotions.GetAll.V2;
using PromotionEngine.Application.Features.Promotions.GetAll.V2.Mappers;
using PromotionEngine.Application.Features.Promotions.GetById.V1;
using PromotionEngine.Application.Features.Promotions.GetById.V1.Mappers;
using PromotionEngine.Application.Shared;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Application.Shared.Mappers;
using PromotionEngine.Application.Shared.Repositories;
using PromotionEngine.Application.Shared.Services;

namespace PromotionEngine.Application.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring services in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers application services in the dependency injection container.
    /// </summary>
    /// <param name="services">The collection of service descriptors to which services are added.</param>
    /// <param name="configuration">The configuration object used to retrieve application settings.</param>
    /// <returns>The updated service collection with the registered services.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // The order of service registration must be maintained.
        services.AddDatabasesConnections(configuration)
                .AddRepositories()
                .AddMappers()
                .AddHandlers()
                .AddValidators();

        return services;
    }

    /// <summary>
    /// Adds database connection services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The configuration object to retrieve connection strings.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDatabasesConnections(this IServiceCollection services, IConfiguration configuration)
    {
        // Register database connection service.
        services.AddSingleton<IDatabaseConnection, DatabaseConnection>(
                provider =>
                {
                    var logger = provider.GetRequiredService<ILogger<DatabaseConnection>>();
                    var connectionString = configuration.GetConnectionString("Database") ?? string.Empty;

                    return new DatabaseConnection(connectionString, logger);
                });

        return services;
    }

    /// <summary>
    /// Adds repository services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPromotionsRepository, PromotionsRepository>();
        return services;
    }

    /// <summary>
    /// Adds mapper services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        // Register mappers.
        services.AddAutoMapper(typeof(DiscountToDiscountModelMappingProfile));
        services.AddAutoMapper(typeof(PromotionsV1MappingProfile));
        services.AddAutoMapper(typeof(PromotionsV2MappingProfile));
        services.AddAutoMapper(typeof(PromotionByIdV1MappingProfile));

        return services;
    }

    /// <summary>
    /// Adds handler services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        // Register the handler for processing promotion requests
        services.AddTransient<IHandler<GetAllPromotionsV1Request, PromotionsV1Response>, GetAllPromotionsV1Handler>();
        services.AddTransient<IHandler<GetAllPromotionsV2Request, PromotionsV2Response>, GetAllPromotionsV2Handler>();
        services.AddTransient<IHandler<GetPromotionByIdV1Request, PromotionByIdV1Response>, GetPromotionByIdV1Handler>();
        return services;
    }

    /// <summary>
    /// Registers all validators from the assembly referenced by <see cref="ApplicationAssemblyReference.Assembly"/>
    /// with the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the validators will be added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> with validators registered.</returns>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly);

        return services;
    }
}