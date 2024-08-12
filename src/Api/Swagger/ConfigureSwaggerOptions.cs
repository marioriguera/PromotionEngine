using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using PromotionEngine.Application.Shared;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PromotionEngine.Swagger;

/// <summary>
/// Configures Swagger generation options for multiple API versions.
/// </summary>
/// <remarks>
/// This class implements <see cref="IConfigureOptions{SwaggerGenOptions}"/> to set up Swagger documentation for different API versions. It uses the <see cref="IApiVersionDescriptionProvider"/> to create documentation for each API version.
/// </remarks>
public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
    /// </summary>
    /// <param name="provider">The API version description provider.</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

    /// <summary>
    /// Configures the Swagger options for the API versions.
    /// </summary>
    /// <param name="options">The Swagger generation options to configure.</param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        // Get the name of the documentation XML file for the current assembly.
        var xmlFile = $"{ApplicationAssemblyReference.Assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        // Including XML comments for Swagger.
        options.IncludeXmlComments(xmlPath);
    }

    /// <summary>
    /// Creates an <see cref="OpenApiInfo"/> object with metadata for the given API version.
    /// </summary>
    /// <param name="description">The API version description containing version information and deprecation status.</param>
    /// <returns>An <see cref="OpenApiInfo"/> object with details for the API version.</returns>
    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var info = new OpenApiInfo()
        {
            Title = "Promotions Management Web API",
            Version = description.ApiVersion.ToString(),
            Description = "Description for the Promotions Management Web API",
            Contact = new OpenApiContact { Name = "Mario David Riguera Castillo", Email = "mario.riguera@gmail.com" },
            License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}