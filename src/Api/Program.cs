using PromotionEngine.Application.DependencyInjection;
using PromotionEngine.DependencyInjection;
using Serilog;

/*
 * The main entry point for the application.
 */

var builder = WebApplication.CreateBuilder(args);

try
{
    // Make the host use serilog.
    builder.Host
           .UseSerilog();

    builder.Services
           .AddApi(builder.Configuration)
           .AddApplication(builder.Configuration);

    var app = builder.Build();
    app.ConfigureWebApplication();

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