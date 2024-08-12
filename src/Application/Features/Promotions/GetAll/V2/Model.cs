using System.ComponentModel.DataAnnotations;
using PromotionEngine.Application.Shared.Abstracts;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

/// <summary>
/// Represents the model for retrieving all promotions in version 2.
/// </summary>
/// <param name="PromotionId">The unique identifier for the promotion.</param>
/// <param name="Status">The current status of the promotion.</param>
/// <param name="CreatedDate">The date when the promotion was created.</param>
/// <param name="EndValidityDate">The date when the promotion ends or becomes invalid.</param>
/// <param name="Texts">The text content associated with the promotion, such as title and description.</param>
/// <param name="Images">A list of image URLs associated with the promotion.</param>
/// <param name="Discounts">A list of discounts applied in the promotion.</param>
public record GetAllPromotionsV2Model(
        [Required] Guid PromotionId,
        [Required] string Status,
        [Required] DateTime CreatedDate,
        DateTime EndValidityDate,
        PromotionTextsModel? Texts,
        List<string> Images,
        List<DiscountModel>? Discounts)
    : PromotionBaseModel(PromotionId, EndValidityDate, Texts, Images, Discounts);
