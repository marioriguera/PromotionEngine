using AutoMapper;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1.Mappers;

/// <summary>
/// Mapping profile for mapping Promotion entity to PromotionModel.
/// </summary>
internal sealed class PromotionsMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionsMappingProfile"/> class.
    /// Configures mappings between <see cref="Promotion"/> and <see cref="PromotionModel"/> using AutoMapper.
    /// Includes mappings for complex properties such as Texts and DisplayContent.
    /// </summary>
    public PromotionsMappingProfile()
    {
        CreateMap<Promotion, PromotionModel>()
            .ForMember(dest => dest.PromotionId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.EndValidityDate, opt => opt.MapFrom(src => src.EndValidityDate))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src => src.Discounts))
            .ForMember(dest => dest.Texts, opt => opt.MapFrom(src => src.DisplayContent != null
                ? new PromotionTextsModel
                {
                    Title = src.DisplayContent.ContainsKey("title") ? src.DisplayContent["title"].Title : null,
                    Description = src.DisplayContent.ContainsKey("description") ? src.DisplayContent["description"].Description : null,
                    DiscountTitle = src.DisplayContent.ContainsKey("discountTitle") ? src.DisplayContent["discountTitle"].DiscountTitle : null,
                    DiscountDescription = src.DisplayContent.ContainsKey("discountDescription") ? src.DisplayContent["discountDescription"].DiscountDescription : null,
                }
                : null));

        CreateMap<PromotionModel, Promotion>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PromotionId))
            .ForMember(dest => dest.EndValidityDate, opt => opt.MapFrom(src => src.EndValidityDate))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src => src.Discounts))
            .ForMember(dest => dest.DisplayContent, opt => opt.MapFrom(src => src.Texts != null
                ? new Dictionary<string, DisplayContent>
                {
                { "title", new DisplayContent { Title = src.Texts.Title } },
                { "description", new DisplayContent { Description = src.Texts.Description } },
                { "discountTitle", new DisplayContent { DiscountTitle = src.Texts.DiscountTitle } },
                { "discountDescription", new DisplayContent { DiscountDescription = src.Texts.DiscountDescription } },
                }
                : null));
    }
}
