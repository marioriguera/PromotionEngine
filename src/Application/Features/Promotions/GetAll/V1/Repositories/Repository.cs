using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1.Repositories;

/// <summary>
/// Repository for accessing promotion data from the database.
/// </summary>
internal sealed class PromotionsRepository : IPromotionsRepository
{
    private readonly IDatabaseConnection _databaseConnectionInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionsRepository"/> class with the specified database connection.
    /// </summary>
    /// <param name="databaseConnectionInfo">The database connection information.</param>
    public PromotionsRepository(IDatabaseConnection databaseConnectionInfo)
    {
        _databaseConnectionInfo = databaseConnectionInfo;
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Promotion> GetAll(string countryCode, string languageCode, CancellationToken cancellationToken)
    {
        using (_databaseConnectionInfo)
        {
            return _databaseConnectionInfo.QueryAsync(_ => _.CountryCode == countryCode && (_.DisplayContent?.ContainsKey(languageCode) ?? false), cancellationToken);
        }
    }
}