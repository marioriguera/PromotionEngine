using Asp.Versioning;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Application.Shared.Interfaces;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Controller for managing promotions.
/// </summary>
[ApiVersion(1.0)]
[Route("api/v{v:apiVersion}/promotions")]
[ControllerName("Get alls promotions.")]
public class GetAllsPromotionsV1Controller : FeatureControllerBase
{
    private readonly IHandler<PromotionsV1Request, PromotionsV1Response>? _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllsPromotionsV1Controller"/> class.
    /// </summary>
    /// <param name="handler">The handler to process promotion requests.</param>
    /// <param name="logger">The logger instance for logging.</param>
    public GetAllsPromotionsV1Controller(
        IHandler<PromotionsV1Request, PromotionsV1Response> handler,
        ILogger<GetAllsPromotionsV1Controller> logger)
        : base(logger)
    {
        _handler = handler;
    }

    /// <summary>
    /// Gets promotions based on the specified country and language codes.
    /// Version 1.0
    /// </summary>
    /// <param name="countryCode">The ISO-3166 ALPHA-2 country code.</param>
    /// <param name="languageCode">The language code for the promotions.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> containing the promotions response or an error.</returns>
    [MapToApiVersion(1.0)]
    [HttpGet("{countryCode}/all")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PromotionsV1Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "PromotionsList", Description = "Get Promotions")]
    public async Task<IActionResult> GetAllPromotionsAsync(
        [SwaggerParameter("ISO-3166 ALPHA-2")] string countryCode,
        string languageCode,
        CancellationToken cancellationToken)
    {
        var response = await _handler!.HandleAsync(
            new PromotionsV1Request(
                                    countryCode,
                                    languageCode), cancellationToken);

        return response.Match(
                promotions => Ok(promotions),
                errors => Problem(errors));
    }
}
