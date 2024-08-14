using ErrorOr;
using Microsoft.AspNetCore.Http;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

/// <summary>
/// Contains unit tests for the <see cref="GetPromotionByIdV1Controller"/>.
/// </summary>
public class GetPromotionByIdV1Tests
{
    private readonly ServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPromotionByIdV1Tests"/> class.
    /// Sets up the service provider to be used in the test methods.
    /// </summary>
    public GetPromotionByIdV1Tests()
    {
        _serviceProvider = ServicesRegisterToTests.Current.GetServiceProvider();
    }

    /// <summary>
    /// Tests the <see cref="GetPromotionByIdV1Controller.GetPromotionByIdAsync"/> method to ensure it returns a successful response
    /// when a valid promotion ID is provided.
    /// </summary>
    [Fact]
    public async Task GetPromotion_ShouldReturnOk_WithValid_PromotionId_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetPromotionByIdV1Controller>();

        OkObjectResult result = (OkObjectResult)await controller!.GetPromotionByIdAsync(new Guid("684d2064-916f-41bb-8ff9-23c83c7f9282"), default);
        PromotionByIdV1Response response = (PromotionByIdV1Response)result.Value!;

        Assert.Equal(200, result.StatusCode);
        Assert.Equal(new Guid("684d2064-916f-41bb-8ff9-23c83c7f9282"), response.promotion.PromotionId);
        Assert.Equal("Disabled", response.promotion.Status.ToString());
        Assert.Equal(DateTime.Parse("2024-08-08T19:31:06.6821825+02:00", null, System.Globalization.DateTimeStyles.RoundtripKind), response.promotion.CreatedDate);
        Assert.Equal(DateTime.Parse("2024-08-08T19:32:46.6392642+02:00", null, System.Globalization.DateTimeStyles.RoundtripKind), response.promotion.LastModifiedDate);
        Assert.Equal(DateTime.Parse("2024-08-08T19:33:35.9697746+02:00", null, System.Globalization.DateTimeStyles.RoundtripKind), response.promotion.EndValidityDate);
        Assert.Equal(2, response.promotion.Images.Count);
        Assert.Contains("Image9", response.promotion.Images);
        Assert.Contains("Image10", response.promotion.Images);
        Assert.Equal("Caribbean Getaway", response.promotion.Texts!.Description);
        Assert.Equal("20% off on all travel packages", response.promotion.Texts!.DiscountDescription);
        Assert.Equal("Cuba Discount", response.promotion.Texts!.DiscountTitle);
        Assert.Equal("Escape to Cuba", response.promotion.Texts!.Title);
        Assert.Equal(2, response.promotion.Discounts!.Count);
    }

    /// <summary>
    /// Tests the <see cref="GetPromotionByIdV1Controller.GetPromotionByIdAsync"/> method to ensure it throws a <see cref="System.FormatException"/>
    /// when no promotion ID is provided.
    /// </summary>
    [Fact]
    public async Task GetPromotion_ShouldBadRequest_WithOut_PromotionId_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetPromotionByIdV1Controller>();

        await Assert.ThrowsAsync<System.FormatException>(async () =>
        {
            ObjectResult action = (ObjectResult)await controller!.GetPromotionByIdAsync(new Guid(string.Empty), default);
        });
    }

    /// <summary>
    /// Tests the <see cref="GetPromotionByIdV1Controller.GetPromotionByIdAsync"/> method to ensure it returns a validation error
    /// when the initial GUID is used as the promotion ID.
    /// </summary>
    [Fact]
    public async Task GetPromotion_ShouldBadRequest_With_initial_PromotionId_TEST()
    {
        // Take controller
        var controller = _serviceProvider.GetService<GetPromotionByIdV1Controller>();

        ObjectResult result = (ObjectResult)await controller!.GetPromotionByIdAsync(new Guid("00000000-0000-0000-0000-000000000000"), default);
        ValidationProblemDetails response = (ValidationProblemDetails)result.Value!;

        Assert.Null(result.StatusCode);
        Assert.Single(response.Errors!);
        Assert.True(response.Errors!.ContainsKey("PromotionId"));
        Assert.Contains("The promotion id cannot be empty.", response.Errors["PromotionId"]);
        Assert.Contains("The GUID is not allowed. GUID: 00000000-0000-0000-0000-000000000000 .", response.Errors["PromotionId"]);
    }
}