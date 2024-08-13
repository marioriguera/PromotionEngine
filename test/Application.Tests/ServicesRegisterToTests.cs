using Microsoft.Extensions.DependencyInjection;
using PromotionEngine.Application.DependencyInjection;

namespace PromotionEngine.Application;

/// <summary>
/// Provides access to a pre-configured <see cref="ServiceProvider"/> for dependency injection in tests.
/// </summary>
/// <remarks>
/// This class is designed to configure and provide a service provider instance that is used across multiple tests.
/// It includes a static instance for easy access and a private constructor to prevent default instantiation.
/// </remarks>
internal sealed class ServicesRegisterToTests
{
    private readonly ServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes static members of the <see cref="ServicesRegisterToTests"/> class.
    /// </summary>
    /// <remarks>
    /// Explicit static constructor to tell the C# compiler not to mark the type as before field initialization.
    /// </remarks>
    static ServicesRegisterToTests()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ServicesRegisterToTests"/> class.
    /// Prevents a default instance of the <see cref="ServicesRegisterToTests"/> class from being created.
    /// </summary>
    private ServicesRegisterToTests()
    {
        // Create services collection.
        var serviceCollection = new ServiceCollection();

        // Add services from application layer.
        serviceCollection.AddLogging()
                         .AddApplicationTests()
                         .AddRepositories()
                         .AddMappers()
                         .AddHandlers()
                         .AddValidators()
                         .AddControllersToTest();

        // Get service provider.
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    /// <summary>
    /// Gets the current instance of <see cref="ServicesRegisterToTests"/> used for accessing the service provider.
    /// </summary>
    /// <value>The singleton instance of <see cref="ServicesRegisterToTests"/>.</value>
    public static ServicesRegisterToTests Current { get; } = new ServicesRegisterToTests();

    /// <summary>
    /// Retrieves the <see cref="ServiceProvider"/> instance used for dependency injection in the tests.
    /// </summary>
    /// <returns>The <see cref="ServiceProvider"/> instance that provides access to registered services.</returns>
    public ServiceProvider GetServiceProvider()
    {
        return _serviceProvider;
    }
}