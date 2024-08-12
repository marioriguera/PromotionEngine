using System.ComponentModel.DataAnnotations;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

public record PromotionV2Model(
        [Required] Guid PromotionId,
        [Required] PromotionStatus Status,
        [Required] DateTime CreatedDate,
        [Required] DateTime EndValidityDate,
        PromotionTextsModel? Texts,
        List<string> Images,
        List<Discount> Discounts)
    : PromotionBaseModel(PromotionId, EndValidityDate, Texts, Images, Discounts);