namespace PromotionEngine.Entities;

/// <summary>
/// Represents a promotional offer with various details, including discounts, display content, and metadata.
/// </summary>
public class Promotion
{
    /// <summary>
    /// Gets or sets the unique identifier for the promotion.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the status of the promotion.
    /// </summary>
    required public PromotionStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the list of discounts applicable to the promotion.
    /// </summary>
    public List<Discount>? Discounts { get; set; }

    /// <summary>
    /// Gets or sets the display content for the promotion, indexed by language code.
    /// </summary>
    public Dictionary<string, DisplayContent>? DisplayContent { get; set; }

    /// <summary>
    /// Gets or sets the country code where the promotion is applicable.
    /// </summary>
    required public string CountryCode { get; set; }

    /// <summary>
    /// Gets or sets the date when the promotion was created.
    /// </summary>
    required public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Gets or sets the date when the promotion was last modified.
    /// </summary>
    required public DateTime LastModifiedDate { get; set; }

    /// <summary>
    /// Gets or sets the end date for the promotion's validity.
    /// </summary>
    public DateTime EndValidityDate { get; set; }

    /// <summary>
    /// Gets or sets the list of image URLs associated with the promotion.
    /// </summary>
    required public List<string> Images { get; set; }
}