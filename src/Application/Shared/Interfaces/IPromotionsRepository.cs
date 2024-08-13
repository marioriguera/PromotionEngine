using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Interfaces;

/// <summary>
/// Defines methods for accessing promotion data from a repository.
/// Version 1.
/// Version 2.
/// </summary>
public interface IPromotionsRepository
{
    /// <summary>
    /// Gets all promotions for a specific country code.
    /// </summary>
    /// <param name="countryCode">The country code to filter promotions.</param>
    /// <param name="languageCode">The language code to filter promotions.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous enumerable of promotions.</returns>
    IAsyncEnumerable<Promotion> GetAll(string countryCode, string languageCode, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all promotions for a specific country and language code, up to a maximum number of promotions.
    /// </summary>
    /// <param name="countryCode">The country code to filter promotions.</param>
    /// <param name="languageCode">The language code to filter promotions based on their display content.</param>
    /// <param name="maxPromotions">The maximum number of promotions to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous enumerable of promotions that match the specified criteria.</returns>
    IAsyncEnumerable<Promotion> GetAll(string countryCode, string languageCode, int maxPromotions, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously retrieves a promotion by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the promotion to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task{Promotion}"/>? representing the asynchronous operation. The task result contains the promotion if found; otherwise, <c>null</c>.</returns>
    Task<Promotion?> GetPromotionByIdAsync(Guid id, CancellationToken cancellationToken);
}
