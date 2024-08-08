using PromotionEngine.Application.Features.Promotions.GetAll.V1.Repositories;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Handles the request for fetching promotions based on the provided country and language codes.
/// </summary>
internal class Handler : IHandler<Request, Response>
{
    private readonly Repository _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="Handler"/> class.
    /// </summary>
    /// <param name="repository">The repository used to access promotion data.</param>
    public Handler(Repository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the asynchronous request to fetch promotions.
    /// </summary>
    /// <param name="request">The request containing country and language codes.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The response containing the list of promotions or an exception if one occurred.</returns>
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = default)
    {
        var response = new Response();

        try
        {
            var promotions = await _repository.GetAll(request.CountryCode, cancellationToken).ToListAsync(cancellationToken);

            var promotionModels = promotions.Select(promotion =>
            {
                var promotionModel = new PromotionModel
                {
                    PromotionId = promotion.Id,
                    EndValidityDate = promotion.EndValidityDate,
                    Discounts = promotion.Discounts?.ToList() ?? [],
                    Images = promotion.Images.ToList()
                };

                if (promotion.DisplayContent != null && promotion.DisplayContent.TryGetValue(request.LanguageCode, out var displayContent))
                {
                    promotionModel.Texts = new PromotionTextsModel
                    {
                        Description = displayContent.Description,
                        DiscountTitle = displayContent.DiscountTitle,
                        Title = displayContent.Title,
                        DiscountDescription = displayContent.DiscountDescription
                    };
                }

                return promotionModel;
            }).ToList();

            return response.SetPromotions(promotionModels);
        }
        catch (Exception ex)
        {
            return response.SetException(ex);
        }
    }
}