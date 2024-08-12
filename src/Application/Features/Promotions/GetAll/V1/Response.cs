using PromotionEngine.Application.Shared.Abstracts;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Response for the version 1 of promotions, containing a collection of promotion models.
/// </summary>
/// <param name="Promotions">The collection of promotions returned in the response.</param>
public record PromotionsV1Response(IEnumerable<PromotionV1Model>? Promotions)
    : PromotionBaseResponse<PromotionV1Model>(Promotions);
