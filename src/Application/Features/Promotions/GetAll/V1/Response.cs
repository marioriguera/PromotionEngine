namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Represents a response containing promotions data and error handling.
/// </summary>
/// <param name="Promotions">The collection of promotions.</param>
public record PromotionsV1Response(IEnumerable<PromotionModel>? Promotions);