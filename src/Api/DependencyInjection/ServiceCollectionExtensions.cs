using System.Text.Json.Serialization;
using System.Text.Json;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using PromotionEngine.Middlewares;
using PromotionEngine.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Serilog;
using FluentValidation.AspNetCore;
using PromotionEngine.Application.Shared;

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
    /// <param name="configuration">The configuration object used to retrieve application settings.</param>
    /// <returns>The updated service collection with API services configured.</returns>
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        // Serilog configuration from appsettings.json .
        Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
           .Enrich.FromLogContext()
           .CreateLogger();

        // Some configurations from "Program".
        services.AddEndpointsApiExplorer()
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                })
                ;

        // Register global exception handling middleware.
        services.AddTransient<GlobalExceptionHandlingMiddleware>();

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

    /// <summary>
    /// Configures the web application with the necessary middleware and endpoints.
    /// </summary>
    /// <param name="app">The web application to configure.</param>
    /// <returns>The configured web application.</returns>
    public static WebApplication ConfigureWebApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var descriptions = app.DescribeApiVersions();

                // Build a swagger endpoint for each discovered API version
                foreach (var description in descriptions)
                {
                    var url = $"/swagger/{description.GroupName}/swagger.json";
                    var name = description.GroupName.ToUpperInvariant();
                    options.SwaggerEndpoint(url, name);
                }

                // Configure SwaggerUI to redirect to the Swagger page on root access
                options.RoutePrefix = string.Empty; // Set the Swagger UI at the root
            });
        }

        app.UseSerilogRequestLogging();
        app.UseAuthorization();
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        app.MapControllers();

        return app;
    }
}