using System.Text.Json;
using System.Text.Json.Serialization;
using PromotionEngine.Application.DependencyInjection;
using PromotionEngine.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    .AddApi()
    .AddApplication(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
