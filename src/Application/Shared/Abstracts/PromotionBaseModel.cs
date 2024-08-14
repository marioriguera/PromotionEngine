using System.ComponentModel.DataAnnotations;

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
    Guid PromotionId,
    DateTime EndValidityDate,
    PromotionTextsModel? Texts,
    List<string> Images,
    List<DiscountModel>? Discounts);

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

/// <summary>
/// Represents a discount model associated with a promotion.
/// </summary>
/// <param name="Type">The type of discount applied.</param>
/// <param name="OriginalPrice">The original price of the item before the discount.</param>
/// <param name="FinalPrice">The final price of the item after the discount.</param>
/// <param name="LowestPriceLast30Days">The lowest price of the item in the last 30 days.</param>
/// <param name="PriceType">The type of price (e.g., regular, promotional).</param>
/// <param name="UnitsToBuy">The number of units that must be purchased to apply the discount.</param>
/// <param name="UnitsToPay">The number of units that must be paid for after applying the discount.</param>
/// <param name="HasPrice">Indicates whether the discount has an associated price.</param>
public record DiscountModel(
    string Type,
    decimal? OriginalPrice,
    decimal? FinalPrice,
    decimal? LowestPriceLast30Days,
    string? PriceType,
    int UnitsToBuy,
    int UnitsToPay,
    bool HasPrice);