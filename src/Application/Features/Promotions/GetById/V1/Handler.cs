using AutoMapper;
using ErrorOr;
using PromotionEngine.Application.Features.Promotions.GetById.V1.Repositories;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

internal sealed class GetByIdV1Handler : IHandler<PromotionByIdV1Request, PromotionByIdV1Response>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetByIdV1Handler> _logger;
    private readonly IGetPromotionV1Repository _repository;

    public GetByIdV1Handler(IMapper mapper, ILogger<GetByIdV1Handler> logger, IGetPromotionV1Repository repository)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    public async Task<ErrorOr<PromotionByIdV1Response>> HandleAsync(PromotionByIdV1Request request, CancellationToken cancellationToken = default)
    {
        Promotion? promotion = await _repository.GetPromotionByIdAsync(request.PromotionId, cancellationToken);

        // Check if promotion was found
        if (promotion is null)
        {
            string message = $"No promotion found for unique identifier {request.PromotionId} .";

            _logger.LogInformation(message);
            return Error.NotFound("Promotion.NotFound", message);
        }

        return new PromotionByIdV1Response(_mapper.Map<PromotionByIdV1Model>(promotion));
    }
}
