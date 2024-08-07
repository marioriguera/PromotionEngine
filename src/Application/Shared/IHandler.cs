namespace PromotionEngine.Application.Shared;

/// <summary>
/// Defines a contract for handling requests of type <typeparamref name="TRequest"/> and returning responses of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public interface IHandler<TRequest, TResponse>
{
    /// <summary>
    /// Asynchronously handles a request and returns a response.
    /// </summary>
    /// <param name="request">The request to handle.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the response.</returns>
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}