using PromotionEngine.Application.Shared.Abstracts;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

/// <summary>
/// Response for the version 2 of promotions, containing a collection of promotion models.
/// </summary>
/// <param name="Promotions">The collection of promotions returned in the response.</param>
/// <param name="CountPromotions">The count number of promotions.</param>
public record PromotionsV2Response(IEnumerable<GetAllPromotionsV2Model>? Promotions, int CountPromotions)
    : PromotionBaseResponse<GetAllPromotionsV2Model>(Promotions);