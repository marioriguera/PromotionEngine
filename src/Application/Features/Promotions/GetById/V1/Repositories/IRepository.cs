using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1.Repositories;

/// <summary>
/// Defines the contract for a repository that provides access to promotions.
/// </summary>
public interface IGetPromotionV1Repository
{
    /// <summary>
    /// Asynchronously retrieves a promotion by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the promotion to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task{Promotion}"/>? representing the asynchronous operation. The task result contains the promotion if found; otherwise, <c>null</c>.</returns>
    Task<Promotion?> GetPromotionByIdAsync(Guid id, CancellationToken cancellationToken);
}