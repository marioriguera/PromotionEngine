using PromotionEngine.Application.Shared.Abstracts;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Represents a request for promotions based on country and language codes.
/// Version 1.
/// </summary>
/// <param name="CountryCode">The country code for filtering promotions.</param>
/// <param name="LanguageCode">The language code for localization of promotion content.</param>
public record PromotionsV1Request(string CountryCode, string LanguageCode)
    : PromotionBaseRequest(CountryCode, LanguageCode);