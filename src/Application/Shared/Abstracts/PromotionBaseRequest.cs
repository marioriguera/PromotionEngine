namespace PromotionEngine.Application.Shared.Abstracts;

/// <summary>
/// Base request for promotion-related operations, containing country and language codes.
/// </summary>
/// <param name="CountryCode">The country code for filtering promotions.</param>
/// <param name="LanguageCode">The language code for localization of promotion content.</param>
public abstract record PromotionBaseRequest(string CountryCode, string LanguageCode);