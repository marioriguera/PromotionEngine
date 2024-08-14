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
        // ToDo: se puede refactorizar el map de PromotionTextsModel y List<DiscountModel>. se repite en varios sitios
        CreateMap<Promotion, PromotionByIdV1Model>()
            .ConvertUsing((src, dest, context) => new PromotionByIdV1Model(
                    src.Id,
                    src.Status.ToString(),
                    src.CreatedDate,
                    src.LastModifiedDate,
                    src.EndValidityDate,
                    src.DisplayContent != null
                            ? new PromotionTextsModel(
                                 src.DisplayContent.TryGetValue(src.CountryCode, out var titleContent) ? titleContent.Title : null,
                                 src.DisplayContent.TryGetValue(src.CountryCode, out var descriptionContent) ? descriptionContent.Description : null,
                                 src.DisplayContent.TryGetValue(src.CountryCode, out var discountTitleContent) ? discountTitleContent.DiscountTitle : null,
                                 src.DisplayContent.TryGetValue(src.CountryCode, out var discountDescriptionContent) ? discountDescriptionContent.DiscountDescription : null)
                            : null,
                    src.Images,
                    src.Discounts != null
                                  ? context.Mapper.Map<List<DiscountModel>>(src.Discounts)
                                  : null));
    }
}
