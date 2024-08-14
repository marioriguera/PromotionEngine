using System.ComponentModel.DataAnnotations;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1;

/// <summary>
/// Represents a request to retrieve a promotion by its ID for version 1 of the API.
/// </summary>
/// <param name="PromotionId">The unique identifier of the promotion to retrieve.</param>
public record GetPromotionByIdV1Request(Guid PromotionId);