using Asp.Versioning;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Application.Shared.Interfaces;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

/// <summary>
/// Controller for managing promotions.
/// </summary>
[ApiVersion(2.0)]
[Route("api/v{v:apiVersion}/promotions")]
[ControllerName("Get alls promotions.")]
public class GetAllsPromotionsV2Controller : FeatureControllerBase
{
    private readonly IHandler<PromotionsV2Request, PromotionsV2Response> _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllsPromotionsV2Controller"/> class.
    /// </summary>
    /// <param name="handler">The handler to process promotion requests.</param>
    /// <param name="logger">The logger instance for logging.</param>
    public GetAllsPromotionsV2Controller(
            IHandler<PromotionsV2Request, PromotionsV2Response> handler,
            ILogger<GetAllsPromotionsV2Controller> logger)
        : base(logger)
    {
        _handler = handler;
    }

    /// <summary>
    /// Gets promotions based on the specified country and language codes.
    /// Version 2.0
    /// </summary>
    /// <param name="countryCode">The ISO-3166 ALPHA-2 country code.</param>
    /// <param name="languageCode">The language code for the promotions.</param>
    /// <param name="maxPromotions">The largest number of promotions to be returned.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> containing the promotions response or an error.</returns>
    [MapToApiVersion(2.0)]
    [HttpGet("{countryCode}/all")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PromotionsV2Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "PromotionsList", Description = "Get Promotions")]
    public async Task<IActionResult> GetAllPromotionsV2Async(
        [SwaggerParameter("ISO-3166 ALPHA-2")] string countryCode,
        string languageCode,
        int maxPromotions,
        CancellationToken cancellationToken)
    {
        var response = await _handler.HandleAsync(
            new PromotionsV2Request(
                                    countryCode,
                                    languageCode,
                                    maxPromotions), cancellationToken);

        return response.Match(
                promotions => Ok(promotions),
                errors => Problem(errors));
    }
}
