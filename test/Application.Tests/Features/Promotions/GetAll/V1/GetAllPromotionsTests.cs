using System.Linq;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Contains unit tests for the <see cref="GetAllPromotionsV1Controller"/> to verify its functionality.
/// </summary>
/// <remarks>
/// This class sets up a test environment by retrieving the service provider from a singleton instance of <see cref="ServicesRegisterToTests"/>.
/// It includes tests to validate the behavior of the <see cref="GetAllPromotionsV1Controller"/> controller.
/// </remarks>
public class GetAllPromotionsV1Tests
{
    private readonly ServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllPromotionsV1Tests"/> class.
    /// Retrieves the <see cref="ServiceProvider"/> from <see cref="ServicesRegisterToTests"/> for dependency injection.
    /// </summary>
    public GetAllPromotionsV1Tests()
    {
        _serviceProvider = ServicesRegisterToTests.Current.GetServiceProvider();
    }

    /// <summary>
    /// Tests the <see cref="GetAllPromotionsV1Controller.GetAllPromotionsAsync"/> method to ensure it returns a successful response
    /// when valid country and language codes are provided.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task GetAllPromotions_ShouldReturnOk_WithValidCountryCodeAndLanguageCodeEqualsES_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetAllPromotionsV1Controller>();

        OkObjectResult result = (OkObjectResult)await controller!.GetAllPromotionsAsync("ES", "ES", default);
        GetAllPromotionsV1Response response = (GetAllPromotionsV1Response)result.Value!;

        Assert.Equal(200, result.StatusCode);
        Assert.Single(response.Promotions!.ToList());
    }

    /// <summary>
    /// Tests the <see cref="GetAllPromotionsV1Controller.GetAllPromotionsAsync"/> method to ensure it returns a successful response
    /// when valid country and language codes are provided.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task GetAllPromotions_ShouldReturnOk_WithValidCountryCodeAndLanguageCodeEqualsDE_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetAllPromotionsV1Controller>();

        OkObjectResult result = (OkObjectResult)await controller!.GetAllPromotionsAsync("DE", "DE", default);
        GetAllPromotionsV1Response response = (GetAllPromotionsV1Response)result.Value!;

        Assert.Equal(200, result.StatusCode);
        Assert.Equal(3, response.Promotions!.ToList().Count);
    }

    /// <summary>
    /// Tests the <see cref="GetAllPromotionsV1Controller.GetAllPromotionsAsync"/> method to ensure it returns a bad request response
    /// when invalid country and language codes are provided.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task GetAllPromotions_ShouldReturnBadRequest_WithNonValidCountryCodeAndLanguageCodeEqualsOne_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetAllPromotionsV1Controller>();

        ObjectResult result = (ObjectResult)await controller!.GetAllPromotionsAsync("1", "1", default);
        ValidationProblemDetails response = (ValidationProblemDetails)result.Value!;

        Assert.Null(result.StatusCode);
        Assert.Equal(2, response.Errors!.Count);
        Assert.True(response.Errors!.Keys.Contains("CountryCode"));
        Assert.Contains("The country code must have a minimum of 2 characters and a maximum of 3.", response.Errors["CountryCode"]);
        Assert.Contains("The country code must not contain numeric characters.", response.Errors["CountryCode"]);
        Assert.True(response.Errors!.Keys.Contains("LanguageCode"));
        Assert.Contains("The language code must have a minimum of 2 characters and a maximum of 3.", response.Errors["LanguageCode"]);
        Assert.Contains("The language code must not contain numeric characters.", response.Errors["LanguageCode"]);
    }
}