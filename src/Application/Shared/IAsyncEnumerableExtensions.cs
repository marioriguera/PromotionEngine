namespace PromotionEngine.Application.Shared;

/// <summary>
/// Extension methods for working with <see cref="IAsyncEnumerable{T}"/>.
/// </summary>
public static class IAsyncEnumerableExtensions
{
    /// <summary>
    /// Converts an <see cref="IAsyncEnumerable{T}"/> to an <see cref="IEnumerable{T}"/> by enumerating all items asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the <see cref="IAsyncEnumerable{T}"/>.</typeparam>
    /// <param name="asyncEnumerable">The asynchronous enumerable to convert.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of items.</returns>
    public static async Task<IEnumerable<T>> ToListAsync<T>(this IAsyncEnumerable<T> asyncEnumerable, CancellationToken cancellationToken)
    {
        var unrolledAsyncEnumerable = new List<T>();
        await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
        {
            unrolledAsyncEnumerable.Add(item);
        }

        return unrolledAsyncEnumerable;
    }
}