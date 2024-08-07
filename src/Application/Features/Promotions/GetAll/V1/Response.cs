namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

/// <summary>
/// Represents a response containing promotions data and error handling.
/// </summary>
public class Response
{
    /// <summary>
    /// Gets the collection of promotions.
    /// </summary>
    public IEnumerable<PromotionModel> Promotions { get; private set; } = Enumerable.Empty<PromotionModel>();

    /// <summary>
    /// Gets a value indicating whether an exception occurred.
    /// </summary>
    [JsonIgnore]
    public bool ExceptionOccurred { get; private set; }

    /// <summary>
    /// Gets the exception that occurred, if any.
    /// </summary>
    [JsonIgnore]
    public Exception? Exception { get; private set; }

    /// <summary>
    /// Sets the exception details for the response.
    /// </summary>
    /// <param name="ex">The exception that occurred.</param>
    /// <returns>The current instance of the <see cref="Response"/> class.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the exception is null.</exception>
    public Response SetException(Exception ex)
    {
        ArgumentNullException.ThrowIfNull(ex, nameof(ex));

        ExceptionOccurred = true;
        Exception = ex;

        return this;
    }

    /// <summary>
    /// Sets the promotions for the response.
    /// </summary>
    /// <param name="promotions">The collection of promotions to set.</param>
    /// <returns>The current instance of the <see cref="Response"/> class.</returns>
    public Response SetPromotions(IEnumerable<PromotionModel> promotions)
    {
        Promotions = promotions;

        return this;
    }
}