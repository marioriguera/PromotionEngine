namespace PromotionEngine.Application.Shared;

public static class IAsyncEnumerableExtensions
{
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
