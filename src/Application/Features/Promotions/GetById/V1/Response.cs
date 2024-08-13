using System.ComponentModel.DataAnnotations;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

/// <summary>
/// Represents the response for getting a promotion by its identifier.
/// </summary>
/// <param name="promotion">The promotion details.</param>
public record PromotionByIdV1Response(PromotionByIdV1Model promotion);
