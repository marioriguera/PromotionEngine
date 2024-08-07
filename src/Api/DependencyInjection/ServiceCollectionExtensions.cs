using Asp.Versioning;
using Microsoft.Extensions.Options;
using PromotionEngine.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PromotionEngine.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring API-related services in the <see cref="IServiceCollection"/>.
/// </summary>
/// <remarks>
/// This class contains methods to configure Swagger, API versioning, and related services required for API documentation and version management.
/// </remarks>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds API-related services to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which the API services are added.</param>
    /// <returns>The updated service collection with API services configured.</returns>
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        // Register the Swagger configuration options.
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        // Add Swagger generation services and configure options.
        services.AddSwaggerGen(options =>
        {
            // Add a custom operation filter which sets default values.
            options.OperationFilter<SwaggerDefaultValues>();
        });

        // Configure API versioning options.
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        // Add API versioning and API explorer services.
        services.AddApiVersioning()
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

        return services;
    }
}