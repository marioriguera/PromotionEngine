﻿using AutoMapper;
using ErrorOr;
using PromotionEngine.Application.Features.Promotions.GetById.V1.Repositories;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

/// <summary>
/// Handler for processing requests to retrieve a promotion by its unique identifier.
/// </summary>
/// <remarks>
/// This class implements the <see cref="IHandler{TRequest, TResponse}"/> interface to handle
/// <see cref="PromotionByIdV1Request"/> requests and return <see cref="PromotionByIdV1Response"/> responses.
/// It uses an <see cref="IMapper"/> for mapping data and an <see cref="IGetPromotionV1Repository"/> 
/// for data retrieval. Logging is done via <see cref="ILogger{T}"/>.
/// </remarks>
internal sealed class GetByIdV1Handler : IHandler<PromotionByIdV1Request, PromotionByIdV1Response>
{
    private readonly IMapper _mapper;
    private readonly ILogger<GetByIdV1Handler> _logger;
    private readonly IGetPromotionV1Repository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetByIdV1Handler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper to convert data objects to response models.</param>
    /// <param name="logger">The logger to record information and errors.</param>
    /// <param name="repository">The repository to access promotion data.</param>
    public GetByIdV1Handler(IMapper mapper, ILogger<GetByIdV1Handler> logger, IGetPromotionV1Repository repository)
    {
        _mapper = mapper;
        _logger = logger;
        _repository = repository;
    }

    /// <summary>
    /// Handles the request to retrieve a promotion by its unique identifier.
    /// </summary>
    /// <param name="request">The request containing the unique identifier of the promotion to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation if needed.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result is an <see cref="ErrorOr{T}"/> 
    /// where <c>T</c> is <see cref="PromotionByIdV1Response"/> if the promotion is found, or an error if not.
    /// </returns>
    public async Task<ErrorOr<PromotionByIdV1Response>> HandleAsync(PromotionByIdV1Request request, CancellationToken cancellationToken = default)
    {
        Promotion? promotion = await _repository.GetPromotionByIdAsync(request.PromotionId, cancellationToken);

        // Check if promotion was found
        if (promotion is null)
        {
            string message = $"No promotion found for unique identifier {request.PromotionId}.";

            _logger.LogInformation(message);
            return Error.NotFound("Promotion.NotFound", message);
        }

        return new PromotionByIdV1Response(_mapper.Map<PromotionByIdV1Model>(promotion));
    }
}