﻿using AutoMapper;
using PromotionEngine.Application.Shared.Abstracts;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1.Mappers;

/// <summary>
/// Mapping profile for mapping Promotion entity to PromotionModel.
/// </summary>
internal sealed class PromotionsV1MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionsV1MappingProfile"/> class.
    /// Configures mappings between <see cref="Promotion"/> and <see cref="PromotionV1Model"/> using AutoMapper.
    /// Includes mappings for complex properties such as Texts and DisplayContent.
    /// </summary>
    public PromotionsV1MappingProfile()
    {
        CreateMap<Promotion, PromotionV1Model>()
                .ConvertUsing((src, dest, context) => new PromotionV1Model(
                    src.Id,
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
