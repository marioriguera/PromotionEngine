namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Represents a request for promotions based on country and language codes.
/// Version 1.
/// </summary>
/// <param name="CountryCode">The ISO-3166 ALPHA-2 country code.</param>
/// <param name="LanguageCode">The language code for the promotions.</param>
public record struct PromotionsV1Request(string CountryCode, string LanguageCode);