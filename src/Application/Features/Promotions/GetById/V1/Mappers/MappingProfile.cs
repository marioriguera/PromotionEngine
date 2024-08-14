using AutoMapper;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1.Mappers;

/// <summary>
/// Mapping profile for mapping Promotion entity to PromotionByIdV1Model.
/// </summary>
internal sealed class PromotionByIdV1MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionByIdV1MappingProfile"/> class.
    /// Configures mappings between <see cref="Promotion"/> and <see cref="PromotionByIdV1Model"/> using AutoMapper.
    /// </summary>
    public PromotionByIdV1MappingProfile()
    {
        CreateMap<Promotion, PromotionByIdV1Model>()
            .ConvertUsing((src, dest, context) => new PromotionByIdV1Model(
                    src.Id,
                    src.Status.ToString(),
                    src.CreatedDate,
                    src.LastModifiedDate,
                    src.EndValidityDate,
                    src.DisplayContent != null && src.DisplayContent.TryGetValue(src.CountryCode, out var content)
                            ? new PromotionTextsModel(
                                content.Title,
                                content.Description,
                                content.DiscountTitle,
                                content.DiscountDescription)
                            : null,
                    src.Images,
                    src.Discounts != null
                                  ? context.Mapper.Map<List<DiscountModel>>(src.Discounts)
                                  : null));
    }
}
