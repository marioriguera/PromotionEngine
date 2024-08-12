using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Interfaces;

/// <summary>
/// Defines an interface for a database connection that supports querying promotions and checking the connection status.
/// </summary>
public interface IDatabaseConnection : IDisposable
{
    /// <summary>
    /// Queries the database asynchronously for promotions that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the promotions.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>An async enumerable of promotions matching the predicate.</returns>
    IAsyncEnumerable<Promotion> QueryAsync(Func<Promotion, bool> predicate, CancellationToken cancellationToken);

    /// <summary>
    /// Queries the database asynchronously for a specified number of promotions that match the given predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the promotions.</param>
    /// <param name="countMaxToTake">The maximum number of promotions to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>An async enumerable of promotions matching the predicate, up to the specified count.</returns>
    IAsyncEnumerable<Promotion> QueryAsync(Func<Promotion, bool> predicate, int countMaxToTake, CancellationToken cancellationToken);

    /// <summary>
    /// Connects to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean result indicating whether the connection was successful.</returns>
    Task<bool> ConnectAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Pings the database to check the connection.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, with a boolean result indicating whether the ping was successful.</returns>
    Task<bool> PingAsync();
}