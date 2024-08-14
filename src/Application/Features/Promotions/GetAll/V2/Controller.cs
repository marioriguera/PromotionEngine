using Asp.Versioning;
using ErrorOr;
using FluentValidation;
using PromotionEngine.Application.DependencyInjection;
using PromotionEngine.Application.Features.Promotions.GetAll.V1;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Application.Shared.Interfaces;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

/// <summary>
/// Controller for managing promotions.
/// </summary>
[ApiVersion(2.0)]
[Route("api/v{v:apiVersion}/promotions")]
[ControllerName("Get alls promotions.")]
public class GetAllPromotionsV2Controller : FeatureControllerBase
{
    private readonly IHandler<GetAllPromotionsV2Request, GetAllPromotionsV2Response> _handler;
    private readonly IValidator<GetAllPromotionsV2Request> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllPromotionsV2Controller"/> class.
    /// </summary>
    /// <param name="handler">The handler used to process promotion requests and generate responses.</param>
    /// <param name="validator">The validator used to validate promotion request data.</param>
    /// <param name="logger">The logger instance used for logging information and errors.</param>
    public GetAllPromotionsV2Controller(
            IHandler<GetAllPromotionsV2Request, GetAllPromotionsV2Response> handler,
            IValidator<GetAllPromotionsV2Request> validator,
            ILogger<GetAllPromotionsV2Controller> logger)
        : base(logger)
    {
        _handler = handler;
        _validator = validator;
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllPromotionsV2Response))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(OperationId = "PromotionsList", Description = "Get Promotions")]
    public async Task<IActionResult> GetAllPromotionsV2Async(
        [SwaggerParameter("ISO-3166 ALPHA-2")] string countryCode,
        string languageCode,
        int maxPromotions,
        CancellationToken cancellationToken)
    {
        var request = new GetAllPromotionsV2Request(countryCode, languageCode, maxPromotions);

        if (await _validator.ExecuteValidateAsync(request, cancellationToken) is List<Error> errorsList)
        {
            return Problem(errorsList);
        }

        var response = await _handler.HandleAsync(request, cancellationToken);

        return response.Match(
                promotions => Ok(promotions),
                errors => Problem(errors));
    }
}
