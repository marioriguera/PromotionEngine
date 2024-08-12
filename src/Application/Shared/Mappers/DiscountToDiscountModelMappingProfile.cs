using AutoMapper;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Mappers;

/// <summary>
/// Provides mapping configurations between Discount entities and their corresponding DiscountModel representations.
/// </summary>
internal sealed class DiscountToDiscountModelMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DiscountToDiscountModelMappingProfile"/> class.
    /// Configures mappings for Discount, OnlineDiscount, and StoreDiscount models.
    /// </summary>
    public DiscountToDiscountModelMappingProfile()
    {
        // Mapping from OnlineDiscount to DiscountModel using custom conversion logic.
        CreateMap<OnlineDiscount, DiscountModel>()
            .ConvertUsing(src => new DiscountModel(
                    src.Type.ToString(),
                    src.OriginalPrice,
                    src.FinalPrice,
                    src.LowestPriceLast30Days,
                    src.PriceType,
                    src.UnitsToBuy,
                    src.UnitsToPay,
                    src.HasPrice));

        // Mapping from StoreDiscount to DiscountModel using custom conversion logic.
        CreateMap<StoreDiscount, DiscountModel>()
            .ConvertUsing(src => new DiscountModel(
                    src.Type.ToString(),
                    src.OriginalPrice,
                    src.FinalPrice,
                    src.LowestPriceLast30Days,
                    src.PriceType,
                    src.UnitsToBuy,
                    src.UnitsToPay,
                    src.HasPrice));

        CreateMap<Discount, DiscountModel>()
            .ConvertUsing((src, dest, context) =>
            {
                return src.Type switch
                {
                    DiscountType.Store => context.Mapper.Map<DiscountModel>(src),
                    DiscountType.Online => context.Mapper.Map<DiscountModel>(src),
                    _ => throw new InvalidOperationException($"Unknown discount type: {src.Type}")
                };
            });
    }
}