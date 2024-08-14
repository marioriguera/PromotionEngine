using Asp.Versioning;
using ErrorOr;
using FluentValidation;
using PromotionEngine.Application.DependencyInjection;
using PromotionEngine.Application.Features.Promotions.GetById.V1;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Application.Shared.Interfaces;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Controller for managing promotions.
/// </summary>
[ApiVersion(1.0)]
[Route("api/v{v:apiVersion}/promotions")]
[ControllerName("Get alls promotions.")]
public class GetAllPromotionsV1Controller : FeatureControllerBase
{
    private readonly IHandler<GetAllPromotionsV1Request, GetAllPromotionsV1Response>? _handler;
    private readonly IValidator<GetAllPromotionsV1Request> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllPromotionsV1Controller"/> class.
    /// </summary>
    /// <param name="handler">The handler for processing <see cref="GetAllPromotionsV1Request"/> and returning <see cref="GetAllPromotionsV1Response"/>.</param>
    /// <param name="validator">The validator for <see cref="GetPromotionByIdV1Model"/>.</param>
    /// <param name="logger">The logger instance for logging messages.</param>
    public GetAllPromotionsV1Controller(
        IHandler<GetAllPromotionsV1Request, GetAllPromotionsV1Response> handler,
        IValidator<GetAllPromotionsV1Request> validator,
        ILogger<GetAllPromotionsV1Controller> logger)
        : base()
    {
        _handler = handler;
        _validator = validator;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllPromotionsV1Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "PromotionsList", Description = "Get Promotions")]
    public async Task<IActionResult> GetAllPromotionsAsync(
        [SwaggerParameter("ISO-3166 ALPHA-2")] string countryCode,
        string languageCode,
        CancellationToken cancellationToken)
    {
        var request = new GetAllPromotionsV1Request(countryCode, languageCode);

        if (await _validator.ExecuteValidateAsync(request, cancellationToken) is List<Error> errorsList)
        {
            return Problem(errorsList);
        }

        var response = await _handler!.HandleAsync(request, cancellationToken);

        return response.Match(
                promotions => Ok(promotions),
                errors => Problem(errors));
    }
}
