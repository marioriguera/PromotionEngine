using PromotionEngine.Application.Shared;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1.Repositories;

/// <summary>
/// Repository for accessing promotion data from the database.
/// </summary>
internal sealed class Repository
{
    private readonly DatabaseConnection _databaseConnectionInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository"/> class with the specified database connection.
    /// </summary>
    /// <param name="databaseConnectionInfo">The database connection information.</param>
    public Repository(DatabaseConnection databaseConnectionInfo)
    {
        _databaseConnectionInfo = databaseConnectionInfo;
    }

    /// <summary>
    /// Gets all promotions for a specific country code.
    /// </summary>
    /// <param name="countryCode">The country code to filter promotions.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>An asynchronous enumerable of promotions.</returns>
    public IAsyncEnumerable<Promotion> GetAll(string countryCode, CancellationToken cancellationToken)
    {
        using (_databaseConnectionInfo)
        {
            return _databaseConnectionInfo.QueryAsync(_ => _.CountryCode == countryCode, cancellationToken);
        }
    }
}