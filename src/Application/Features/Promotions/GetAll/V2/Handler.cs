using AutoMapper;
using ErrorOr;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

/// <summary>
/// Handler for processing version 2 promotion requests.
/// </summary>
internal sealed class PromotionsV2Handler : IHandler<PromotionsV2Request, PromotionsV2Response>
{
    private readonly IMapper _mapper;
    private readonly ILogger<PromotionsV2Handler> _logger;
    private readonly IPromotionsRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionsV2Handler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper to convert between domain models and response models.</param>
    /// <param name="logger">The logger for logging information and errors.</param>
    /// <param name="repository">The repository to access promotion data.</param>
    public PromotionsV2Handler(IMapper mapper, ILogger<PromotionsV2Handler> logger, IPromotionsRepository repository)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    /// <summary>
    /// Handles the promotion request asynchronously.
    /// </summary>
    /// <param name="request">The request containing the parameters for fetching promotions.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation, with a result of <see cref="ErrorOr{PromotionsV2Response}"/> containing the response or an error.</returns>
    public async Task<ErrorOr<PromotionsV2Response>> HandleAsync(PromotionsV2Request request, CancellationToken cancellationToken = default)
    {
        // Retrieve promotions based on the request parameters
        IEnumerable<Promotion> promotions = await _repository.GetAll(request.CountryCode, request.LanguageCode, request.MaxPromotions, cancellationToken)
                                                             .ToListAsync(cancellationToken)
                                                             ??
                                                             [];

        // Check if any promotions were found
        if (!promotions.Any())
        {
            string message = $"No promotions found for country code {request.CountryCode}, " +
                $"language code {request.LanguageCode} " +
                $"and max count of promotions {request.MaxPromotions}.";

            _logger.LogInformation(message);
            return Error.NotFound("Promotions.NotFound", message);
        }

        // Map the promotions to the response model
        var response = _mapper.Map<List<GetAllPromotionsV2Model>>(promotions);

        return new PromotionsV2Response(response, response.Count);
    }
}