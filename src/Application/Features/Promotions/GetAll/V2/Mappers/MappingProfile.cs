using AutoMapper;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2.Mappers;

/// <summary>
/// AutoMapper profile for mapping between <see cref="Promotion"/> and <see cref="GetAllPromotionsV2Model"/>.
/// </summary>
internal sealed class PromotionsV2MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionsV2MappingProfile"/> class.
    /// Configures mappings between <see cref="Promotion"/> and <see cref="GetAllPromotionsV2Model"/>.
    /// </summary>
    public PromotionsV2MappingProfile()
    {
        CreateMap<Promotion, GetAllPromotionsV2Model>()
                .ConvertUsing((src, dest, context) => new GetAllPromotionsV2Model(
                    src.Id,
                    src.Status.ToString(),
                    src.CreatedDate,
                    src.EndValidityDate,
                    src.DisplayContent != null && src.DisplayContent.TryGetValue(src.CountryCode, out var content)
                            ? new PromotionTextsModel(
                                content.Title,
                                content.Description,
                                content.DiscountTitle,
                                content.DiscountDescription)
                            : null,
                    src.Images,
                    src.Discounts != null ? context.Mapper.Map<List<DiscountModel>>(src.Discounts) : null));
    }
}