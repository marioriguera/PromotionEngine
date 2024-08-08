using System.Text.Json;
using System.Text.Json.Serialization;
using PromotionEngine.Application.DependencyInjection;
using PromotionEngine.DependencyInjection;
using PromotionEngine.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Serilog configuration from appsettings.json .
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    builder.Host.UseSerilog();

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

    app.UseSerilogRequestLogging();
    app.UseAuthorization();
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.MapControllers();

    Log.Information($"Starting up the PromotionEngine application");

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "The PromotionEngine application failed to start correctly");
}
finally
{
    Log.Information($"Shutting down the PromotionEngine application");
    Log.CloseAndFlush();
}