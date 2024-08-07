using PromotionEngine.Application.Features.Promotions.GetAll.V1.Repositories;
using PromotionEngine.Application.Shared;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Handles the request for fetching promotions based on the provided country and language codes.
/// </summary>
public class Handler : IHandler<Request, Response>
{
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
            var databaseConnection = new DatabaseConnection("database://cloudserver.databases.azure:8074/promotion-engine@chj458$@djks");
            await databaseConnection.ConnectAsync(cancellationToken);

            var repository = new Repository(databaseConnection);

            var promotions = await repository.GetAll(request.CountryCode, cancellationToken).ToListAsync(cancellationToken);

            var promotionModels = promotions.Select(promotion =>
            {
                var promotionModel = new PromotionModel
                {
                    PromotionId = promotion.Id,
                    EndValidityDate = promotion.EndValidityDate,
                    Discounts = promotion.Discounts?.ToList() ?? new List<Discount>(),
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