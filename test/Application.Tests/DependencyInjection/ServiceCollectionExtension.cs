namespace PromotionEngine.Application.DependencyInjection;

/// <summary>
/// Provides extension methods for adding application-specific services to the <see cref="IServiceCollection"/>.
/// </summary>
/// <remarks>
/// This static class contains extension methods that allow for easy registration of services related to application testing
/// within an ASP.NET Core dependency injection container.
/// </remarks>
internal static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds the necessary services for application testing to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the testing services will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the registered testing services.</returns>
    public static IServiceCollection AddApplicationTests(this IServiceCollection services)
    {
        services.AddSingleton<IDatabaseConnection, DatabaseInfoToTests>();
        return services;
    }

    /// <summary>
    /// Registers controllers required for testing into the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the controllers will be added.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the registered controllers.</returns>
    public static IServiceCollection AddControllersToTest(this IServiceCollection services)
    {
        services.AddSingleton<GetAllsPromotionsV1Controller>();
        services.AddSingleton<GetAllsPromotionsV2Controller>();
        services.AddSingleton<GetPromotionsByIdV1Controller>();
        return services;
    }
}