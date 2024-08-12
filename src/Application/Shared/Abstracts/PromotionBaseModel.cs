using System.ComponentModel.DataAnnotations;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Abstracts;

/// <summary>
/// Represents the base model for a promotion, containing common properties shared across different promotion models.
/// </summary>
/// <param name="PromotionId">The unique identifier of the promotion.</param>
/// <param name="EndValidityDate">The date when the promotion ends.</param>
/// <param name="Texts">The textual content related to the promotion.</param>
/// <param name="Images">A list of image URLs associated with the promotion.</param>
/// <param name="Discounts">A list of discounts available in the promotion.</param>
public abstract record PromotionBaseModel(
    [Required] Guid PromotionId,
    [Required] DateTime EndValidityDate,
    PromotionTextsModel? Texts,
    List<string> Images,
    List<Discount> Discounts);

/// <summary>
/// Represents the textual content for a promotion, including title and descriptions.
/// </summary>
/// <param name="Title">The title of the promotion.</param>
/// <param name="Description">The description of the promotion.</param>
/// <param name="DiscountTitle">The title for the discount information.</param>
/// <param name="DiscountDescription">The description for the discount information.</param>
public record PromotionTextsModel(
    string? Title,
    string? Description,
    string? DiscountTitle,
    string? DiscountDescription);
