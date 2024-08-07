using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.Application.Shared;

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
        // Register the handler for processing promotion requests
        services.AddTransient<IHandler<Features.Promotions.GetAll.V1.Request, Features.Promotions.GetAll.V1.Response>, Features.Promotions.GetAll.V1.Handler>();

        return services;
    }
}