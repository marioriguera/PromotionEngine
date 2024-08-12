using AutoMapper;
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
            .ConvertUsing(src => new PromotionByIdV1Model(
                    src.Id,
                    src.Status,
                    src.CreatedDate,
                    src.LastModifiedDate,
                    src.EndValidityDate,
                    src.Images));
    }
}
