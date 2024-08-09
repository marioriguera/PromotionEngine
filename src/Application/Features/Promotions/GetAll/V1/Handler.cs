using AutoMapper;
using ErrorOr;
using PromotionEngine.Application.Features.Promotions.GetAll.V1.Repositories;
using PromotionEngine.Application.Shared;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Handles the request for fetching promotions based on the provided country and language codes.
/// </summary>
internal class PromotionsV1Handler : IHandler<PromotionsV1Request, PromotionsV1Response>
{
    private readonly IMapper _mapper;
    private readonly PromotionsRepository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionsV1Handler"/> class.
    /// </summary>
    /// <param name="mapper">The mapper to convert data models to response models.</param>
    /// <param name="repository">The repository used to access promotion data.</param>
    public PromotionsV1Handler(IMapper mapper, PromotionsRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    /// <summary>
    /// Handles the asynchronous request to fetch promotions.
    /// </summary>
    /// <param name="request">The request containing country and language codes.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The response containing the list of promotions or an exception if one occurred.</returns>
    public async Task<ErrorOr<PromotionsV1Response>> HandleAsync(PromotionsV1Request request, CancellationToken cancellationToken = default)
    {
        /*
         * If it is not necessary to handle exceptions unless it is a system
         * requirement we can leave the responsibility of handling exceptions to a Middleware.
         * I have removed the try/catch from this function.
         */
        IEnumerable<Promotion>? promotions = await _repository.GetAll(request.CountryCode, cancellationToken)
                                                              .ToListAsync(cancellationToken)
                                                              ??
                                                              [];
        if (!promotions.Any())
        {
            return Error.NotFound("Promotions.NotFound", "The promotions with the provide country code was not found.");
        }

        // var promotionModels = promotions.Select(promotion =>
        // {
        //     var promotionModel = new PromotionModel
        //     {
        //         PromotionId = promotion.Id,
        //         EndValidityDate = promotion.EndValidityDate,
        //         Discounts = promotion.Discounts?.ToList() ?? [],
        //         Images = promotion.Images.ToList()
        //     };
        // 
        //     if (promotion.DisplayContent != null && promotion.DisplayContent.TryGetValue(request.LanguageCode, out var displayContent))
        //     {
        //         promotionModel.Texts = new PromotionTextsModel
        //         {
        //             Description = displayContent.Description,
        //             DiscountTitle = displayContent.DiscountTitle,
        //             Title = displayContent.Title,
        //             DiscountDescription = displayContent.DiscountDescription
        //         };
        //     }
        // 
        //     return promotionModel;
        // }).ToList();

        return new PromotionsV1Response(_mapper.Map<List<PromotionModel>>(promotions));
    }
}