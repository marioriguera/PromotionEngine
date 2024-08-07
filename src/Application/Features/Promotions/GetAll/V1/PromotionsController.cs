using Asp.Versioning;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Controller for managing promotions.
/// </summary>
[ApiVersion(1.0)]
[ApiVersion(2.0)]
[Route("api/v{v:apiVersion}/promotions")]
public class PromotionsController : FeatureControllerBase
{
    private readonly IHandler<Request, Response> _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionsController"/> class.
    /// </summary>
    /// <param name="handler">The handler to process promotion requests.</param>
    /// <param name="logger">The logger instance for logging.</param>
    public PromotionsController(
        IHandler<Request, Response> handler,
        ILogger<PromotionsController> logger)
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "PromotionsList", Description = "Get Promotions")]
    public async Task<IActionResult> GetAllPromotionsV1Async(
        [SwaggerParameter("ISO-3166 ALPHA-2")] string countryCode,
        string languageCode,
        CancellationToken cancellationToken)
    {
        var request = new Request(countryCode, languageCode);

        var response = await _handler.HandleAsync(request, cancellationToken);

        return response switch
        {
            { ExceptionOccurred: true, Exception: var exception } => HandleException(exception!),
            _ => Ok(response)
        };
    }

    /// <summary>
    /// Gets promotions based on the specified country and language codes.
    /// Version 2.0
    /// </summary>
    /// <param name="countryCode">The ISO-3166 ALPHA-2 country code.</param>
    /// <param name="languageCode">The language code for the promotions.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> containing the promotions response or an error.</returns>
    [MapToApiVersion(2.0)]
    [HttpGet("{countryCode}/all")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "PromotionsList", Description = "Get Promotions")]
    public async Task<IActionResult> GetAllPromotionsV2Async(
        [SwaggerParameter("ISO-3166 ALPHA-2")] string countryCode,
        string languageCode,
        CancellationToken cancellationToken)
    {
        var request = new Request(countryCode, languageCode);

        var response = await _handler.HandleAsync(request, cancellationToken);

        return response switch
        {
            { ExceptionOccurred: true, Exception: var exception } => HandleException(exception!),
            _ => Ok(response)
        };
    }
}
