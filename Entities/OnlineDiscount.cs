namespace PromotionEngine.Entities;

/// <summary>
/// Represents an online discount with various pricing details and conditions.
/// </summary>
public class OnlineDiscount : Discount
{
    /// <summary>
    /// Gets the type of discount, which is <see cref="DiscountType.Online"/>.
    /// </summary>
    public override DiscountType Type => DiscountType.Online;

    /// <summary>
    /// Gets or sets the original price of the item before the discount.
    /// </summary>
    public decimal? OriginalPrice { get; set; }

    /// <summary>
    /// Gets or sets the final price of the item after applying the discount.
    /// </summary>
    public decimal? FinalPrice { get; set; }

    /// <summary>
    /// Gets or sets the lowest price of the item in the last 30 days.
    /// </summary>
    public decimal? LowestPriceLast30Days { get; set; }

    /// <summary>
    /// Gets or sets the type of price (e.g., percentage, flat rate).
    /// </summary>
    public string? PriceType { get; set; }

    /// <summary>
    /// Gets or sets the number of units that must be bought to apply the discount.
    /// </summary>
    public int UnitsToBuy { get; set; }

    /// <summary>
    /// Gets or sets the number of units that must be paid for to receive the discount.
    /// </summary>
    public int UnitsToPay { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the discount has a price associated with it.
    /// </summary>
    public bool HasPrice { get; set; }
}