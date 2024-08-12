using PromotionEngine.Application.Features.Promotions.GetAll.V1;

namespace PromotionEngine.Application.Shared.Abstracts;

/// <summary>
/// Represents a response containing promotions data.
/// </summary>
/// <param name="Promotions">The collection of promotions.</param>
public abstract record PromotionBaseResponse<T>(IEnumerable<T>? Promotions);
