using System.ComponentModel.DataAnnotations;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Represents the version 1 model of a promotion, inheriting common properties from the base promotion model.
/// </summary>
/// <param name="PromotionId">The unique identifier of the promotion.</param>
/// <param name="EndValidityDate">The date when the promotion ends.</param>
/// <param name="Texts">The textual content related to the promotion.</param>
/// <param name="Images">A list of image URLs associated with the promotion.</param>
/// <param name="Discounts">A list of discounts available in the promotion.</param>
public record PromotionV1Model(
        [Required] Guid PromotionId,
        [Required] DateTime EndValidityDate,
        PromotionTextsModel? Texts,
        List<string> Images,
        List<DiscountModel>? Discounts)
    : PromotionBaseModel(PromotionId, EndValidityDate, Texts, Images, Discounts);