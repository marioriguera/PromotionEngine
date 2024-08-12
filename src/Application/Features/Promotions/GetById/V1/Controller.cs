using Asp.Versioning;
using PromotionEngine.Application.Features.Promotions.GetAll.V1;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Application.Shared.Interfaces;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

/// <summary>
/// Controller for managing promotions.
/// </summary>
[ApiVersion(1.0)]
[Route("api/v{v:apiVersion}/promotions")]
[ControllerName("Get promotion by id.")]
public class GetPromotionsByIdV1Controller : FeatureControllerBase
{
    private readonly IHandler<PromotionsV1Request, PromotionsV1Response>? _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPromotionsByIdV1Controller"/> class.
    /// </summary>
    /// <param name="handler">The handler to process promotion requests.</param>
    /// <param name="logger">The logger instance for logging.</param>
    public GetPromotionsByIdV1Controller(
        IHandler<PromotionsV1Request, PromotionsV1Response> handler,
        ILogger<GetPromotionsByIdV1Controller> logger)
        : base(logger)
    {
        _handler = handler;
    }

    /// <summary>
    /// Get promotion based on the promotion unique identifier.
    /// Version 1.0
    /// </summary>
    /// <param name="promotionId">The promotion unique identifier.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> containing the promotions response or an error.</returns>
    [MapToApiVersion(1.0)]
    [HttpGet("by-id/{promotionId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "Promotion", Description = "Get Promotion by Id")]
    public async Task<IActionResult> GetPromotionByIdAsync(
        Guid promotionId,
        CancellationToken cancellationToken)
    {
        var response = string.Empty;

        return Ok();
    }
}
