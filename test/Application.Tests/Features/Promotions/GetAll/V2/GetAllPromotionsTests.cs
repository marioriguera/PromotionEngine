namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

/// <summary>
/// Contains unit tests for the <see cref="GetAllsPromotionsV2Controller"/> to verify its functionality.
/// </summary>
/// <remarks>
/// This class sets up a test environment by retrieving the service provider from a singleton instance of <see cref="ServicesRegisterToTests"/>.
/// It includes tests to validate the behavior of the <see cref="GetAllsPromotionsV2Controller"/> controller.
/// </remarks>
public class GetAllPromotionsV2Tests
{
    private readonly ServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllPromotionsV2Tests"/> class.
    /// Retrieves the <see cref="ServiceProvider"/> from <see cref="ServicesRegisterToTests"/> for dependency injection.
    /// </summary>
    public GetAllPromotionsV2Tests()
    {
        _serviceProvider = ServicesRegisterToTests.Current.GetServiceProvider();
    }

    /// <summary>
    /// Tests the <see cref="GetAllsPromotionsV2Controller.GetAllPromotionsV2Async"/> method to ensure it returns a successful response
    /// when valid country and language codes are provided along with a valid number of promotions.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task GetAllPromotions_ShouldReturnOk_WithValidCountryCodeAndLanguageCodeEqualsES_MaxPromotionsEqualsFour_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetAllsPromotionsV2Controller>();

        OkObjectResult result = (OkObjectResult)await controller!.GetAllPromotionsV2Async("DE", "DE", 4, default);
        PromotionsV2Response response = (PromotionsV2Response)result.Value!;

        Assert.Equal(200, result.StatusCode);
        Assert.Equal(3, response.CountPromotions);
    }

    /// <summary>
    /// Tests the <see cref="GetAllsPromotionsV2Controller.GetAllPromotionsV2Async"/> method to ensure it returns a bad request response
    /// when invalid country and language codes are provided along with a maximum number of promotions set to 100.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task GetAllPromotions_ShouldReturnBadRequest_WithInvalidCountryAndLanguagueCodeAnd_MaxPromotions_Equals_100_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetAllsPromotionsV2Controller>();

        ObjectResult result = (ObjectResult)await controller!.GetAllPromotionsV2Async("1", "1", 100, default);
        ValidationProblemDetails response = (ValidationProblemDetails)result.Value!;

        Assert.Null(result.StatusCode);
        Assert.Equal(3, response.Errors!.Count);
        Assert.True(response.Errors!.Keys.Contains("CountryCode"));
        Assert.Contains("The country code must have a minimum of 2 characters and a maximum of 3.", response.Errors["CountryCode"]);
        Assert.Contains("The country code must not contain numeric characters.", response.Errors["CountryCode"]);
        Assert.True(response.Errors!.Keys.Contains("LanguageCode"));
        Assert.Contains("The language code must have a minimum of 2 characters and a maximum of 3.", response.Errors["LanguageCode"]);
        Assert.Contains("The language code must not contain numeric characters.", response.Errors["LanguageCode"]);
        Assert.True(response.Errors!.Keys.Contains("MaxPromotions"));
        Assert.Contains("You can apply for a maximum of 50 promotions.", response.Errors["MaxPromotions"]);
    }

    /// <summary>
    /// Tests the <see cref="GetAllsPromotionsV2Controller.GetAllPromotionsV2Async"/> method to ensure it returns a bad request response
    /// when invalid country and language codes are provided along with a maximum number of promotions set to 0.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task GetAllPromotions_ShouldReturnBadRequest_WithInvalidCountryAndLanguagueCodeAnd_MaxPromotions_Equals_0_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetAllsPromotionsV2Controller>();

        ObjectResult result = (ObjectResult)await controller!.GetAllPromotionsV2Async("1", "1", 0, default);
        ValidationProblemDetails response = (ValidationProblemDetails)result.Value!;

        Assert.Null(result.StatusCode);
        Assert.Equal(3, response.Errors!.Count);
        Assert.True(response.Errors!.Keys.Contains("CountryCode"));
        Assert.Contains("The country code must have a minimum of 2 characters and a maximum of 3.", response.Errors["CountryCode"]);
        Assert.Contains("The country code must not contain numeric characters.", response.Errors["CountryCode"]);
        Assert.True(response.Errors!.Keys.Contains("LanguageCode"));
        Assert.Contains("The language code must have a minimum of 2 characters and a maximum of 3.", response.Errors["LanguageCode"]);
        Assert.Contains("The language code must not contain numeric characters.", response.Errors["LanguageCode"]);
        Assert.True(response.Errors!.Keys.Contains("MaxPromotions"));
        Assert.Contains("You must apply for at least one promotion.", response.Errors["MaxPromotions"]);
    }

    /// <summary>
    /// Tests the <see cref="GetAllsPromotionsV2Controller.GetAllPromotionsV2Async"/> method to ensure it returns a bad request response
    /// when a valid country and language code are provided but the maximum number of promotions is set to 100, which exceeds the allowed limit.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task GetAllPromotions_ShouldReturnBadRequest_WithInvalid_MaxPromotions_Equals_100_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetAllsPromotionsV2Controller>();

        ObjectResult result = (ObjectResult)await controller!.GetAllPromotionsV2Async("ES", "ES", 100, default);
        ValidationProblemDetails response = (ValidationProblemDetails)result.Value!;

        Assert.Null(result.StatusCode);
        Assert.Single(response.Errors!);
        Assert.True(response.Errors!.Keys.Contains("MaxPromotions"));
        Assert.Contains("You can apply for a maximum of 50 promotions.", response.Errors["MaxPromotions"]);
    }

    /// <summary>
    /// Tests the <see cref="GetAllsPromotionsV2Controller.GetAllPromotionsV2Async"/> method to ensure it returns a bad request response
    /// when a valid country and language code are provided but the maximum number of promotions is set to 0, which is below the allowed minimum.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task GetAllPromotions_ShouldReturnBadRequest_WithInvalid_MaxPromotions_Equals_0_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetAllsPromotionsV2Controller>();

        ObjectResult result = (ObjectResult)await controller!.GetAllPromotionsV2Async("ES", "ES", 0, default);
        ValidationProblemDetails response = (ValidationProblemDetails)result.Value!;

        Assert.Null(result.StatusCode);
        Assert.Single(response.Errors!);
        Assert.True(response.Errors!.Keys.Contains("MaxPromotions"));
        Assert.Contains("You must apply for at least one promotion.", response.Errors["MaxPromotions"]);
    }

    /// <summary>
    /// Tests the <see cref="GetAllsPromotionsV2Controller.GetAllPromotionsV2Async"/> method to ensure it returns a bad request response
    /// when a valid country code is provided but an invalid language code with a value of "1" is provided.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task GetAllPromotions_ShouldReturnBadRequest_WithInvalid_LanguageCode_Equals_1_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetAllsPromotionsV2Controller>();

        ObjectResult result = (ObjectResult)await controller!.GetAllPromotionsV2Async("ES", "1", 10, default);
        ValidationProblemDetails response = (ValidationProblemDetails)result.Value!;

        Assert.Null(result.StatusCode);
        Assert.Single(response.Errors!);
        Assert.True(response.Errors!.Keys.Contains("LanguageCode"));
        Assert.Contains("The language code must have a minimum of 2 characters and a maximum of 3.", response.Errors["LanguageCode"]);
        Assert.Contains("The language code must not contain numeric characters.", response.Errors["LanguageCode"]);
    }
}