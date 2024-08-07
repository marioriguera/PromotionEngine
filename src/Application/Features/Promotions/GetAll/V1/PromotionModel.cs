using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Represents the model for a promotion with additional details for presentation.
/// </summary>
public class PromotionModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the promotion.
    /// </summary>
    required public Guid PromotionId { get; set; }

    /// <summary>
    /// Gets or sets the end date for the promotion's validity.
    /// </summary>
    required public DateTime EndValidityDate { get; set; }

    /// <summary>
    /// Gets or sets the textual content associated with the promotion.
    /// </summary>
    public PromotionTextsModel? Texts { get; set; }

    /// <summary>
    /// Gets or sets the list of image URLs associated with the promotion.
    /// </summary>
    public List<string> Images { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of discounts applicable to the promotion.
    /// </summary>
    public List<Discount>? Discounts { get; set; }
}

/// <summary>
/// Represents the textual content for a promotion.
/// </summary>
public class PromotionTextsModel
{
    /// <summary>
    /// Gets or sets the title of the promotion.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the promotion.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the discount title for the promotion.
    /// </summary>
    public string? DiscountTitle { get; set; }

    /// <summary>
    /// Gets or sets the discount description for the promotion.
    /// </summary>
    public string? DiscountDescription { get; set; }
}