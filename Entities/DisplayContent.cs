namespace PromotionEngine.Entities;

/// <summary>
/// Represents the display content for a promotion, including titles and descriptions.
/// </summary>
public class DisplayContent
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
    /// Gets or sets the title of the discount associated with the promotion.
    /// </summary>
    public string? DiscountTitle { get; set; }

    /// <summary>
    /// Gets or sets the description of the discount associated with the promotion.
    /// </summary>
    public string? DiscountDescription { get; set; }
}