using System.Text.Json.Serialization;
using PromotionEngine.Entities.Converters;

namespace PromotionEngine.Entities;

/// <summary>
/// Abstract base class representing a discount.
/// </summary>
[JsonConverter(typeof(PromotionDiscountJsonConverter))]
public abstract class Discount
{
    /// <summary>
    /// Gets the type of discount.
    /// </summary>
    public abstract DiscountType Type { get; }
}