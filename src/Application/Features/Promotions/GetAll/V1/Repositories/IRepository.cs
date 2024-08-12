using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1.Repositories;

/// <summary>
/// Defines methods for accessing promotion data from a repository.
/// Version 1.
/// </summary>
public interface IPromotionsV1Repository
{
    /// <summary>
    /// Gets all promotions for a specific country code.
    /// </summary>
    /// <param name="countryCode">The country code to filter promotions.</param>
    /// <param name="languageCode">The language code to filter promotions.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous enumerable of promotions.</returns>
    IAsyncEnumerable<Promotion> GetAll(string countryCode, string languageCode, CancellationToken cancellationToken);
}