using System.ComponentModel.DataAnnotations;
using Asp.Versioning;
using ErrorOr;
using FluentValidation;
using PromotionEngine.Application.DependencyInjection;
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
public class GetPromotionByIdV1Controller : FeatureControllerBase
{
    private readonly IHandler<PromotionByIdV1Request, PromotionByIdV1Response> _handler;
    private readonly IValidator<PromotionByIdV1Request> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPromotionByIdV1Controller"/> class.
    /// </summary>
    /// <param name="handler">The handler to process promotion requests.</param>
    /// <param name="validator">The validator for <see cref="PromotionByIdV1Request"/>.</param>
    /// <param name="logger">The logger instance for logging.</param>
    public GetPromotionByIdV1Controller(
        IHandler<PromotionByIdV1Request, PromotionByIdV1Response> handler,
        IValidator<PromotionByIdV1Request> validator,
        ILogger<GetPromotionByIdV1Controller> logger)
        : base(logger)
    {
        _handler = handler;
        _validator = validator;
    }

    /// <summary>
    /// Get promotion based on the promotion unique identifier.
    /// Version 1.0
    /// </summary>
    /// <param name="promotionId">The promotion unique identifier.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> containing the promotions response or an error.</returns>
    [MapToApiVersion(1.0)]
    [HttpGet("by-id")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "Promotion", Description = "Get Promotion by Id")]
    public async Task<IActionResult> GetPromotionByIdAsync(
        [Required] Guid promotionId,
        CancellationToken cancellationToken)
    {
        var request = new PromotionByIdV1Request(promotionId);

        if (await _validator.ExecuteValidateAsync(request, cancellationToken) is List<Error> errorsList)
        {
            return Problem(errorsList);
        }

        var response = await _handler.HandleAsync(request, cancellationToken);

        return response.Match(
                promotion => Ok(promotion),
                errors => Problem(errors));
    }
}
