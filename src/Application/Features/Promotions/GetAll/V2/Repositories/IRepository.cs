using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2.Repositories;

/// <summary>
/// Interface for retrieving version 2 promotions from the database.
/// </summary>
public interface IPromotionsV2Repository
{
    /// <summary>
    /// Retrieves all promotions for a specific country and language code, up to a maximum number of promotions.
    /// </summary>
    /// <param name="countryCode">The country code to filter promotions.</param>
    /// <param name="languageCode">The language code to filter promotions based on their display content.</param>
    /// <param name="maxPromotions">The maximum number of promotions to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous enumerable of promotions that match the specified criteria.</returns>
    IAsyncEnumerable<Promotion> GetAll(string countryCode, string languageCode, int maxPromotions, CancellationToken cancellationToken);
}