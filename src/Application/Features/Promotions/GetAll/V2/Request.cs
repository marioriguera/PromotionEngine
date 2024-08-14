using PromotionEngine.Application.Shared.Abstracts;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

/// <summary>
/// Request for retrieving promotions with a specified maximum number of promotions.
/// </summary>
/// <param name="CountryCode">The country code for filtering promotions.</param>
/// <param name="LanguageCode">The language code for localization of promotion content.</param>
/// <param name="MaxPromotions">The maximum number of promotions to retrieve.</param>
public record GetAllPromotionsV2Request(string CountryCode, string LanguageCode, int MaxPromotions)
    : PromotionBaseRequest(CountryCode, LanguageCode);