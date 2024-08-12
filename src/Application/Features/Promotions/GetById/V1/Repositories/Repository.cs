using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1.Repositories;

/// <summary>
/// Implementation of the repository for retrieving promotions.
/// </summary>
internal sealed class GetPromotionV1Repository : IGetPromotionV1Repository
{
    private readonly IDatabaseConnection _databaseConnectionInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPromotionV1Repository"/> class.
    /// </summary>
    /// <param name="databaseConnectionInfo">The database connection information.</param>
    public GetPromotionV1Repository(IDatabaseConnection databaseConnectionInfo)
    {
        _databaseConnectionInfo = databaseConnectionInfo;
    }

    /// <inheritdoc/>
    public async Task<Promotion?> GetPromotionByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        using (_databaseConnectionInfo)
        {
            return (await _databaseConnectionInfo.QueryAsync(_ => _.Id.Equals(id), cancellationToken).ToListAsync(cancellationToken)).FirstOrDefault();
        }
    }
}