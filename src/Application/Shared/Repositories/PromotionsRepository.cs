using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Repositories;

/// <summary>
/// Repository for accessing promotion data from the database.
/// Version 1.
/// Version 2.
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

    /// <inheritdoc/>
    public IAsyncEnumerable<Promotion> GetAll(string countryCode, string languageCode, int maxPromotions, CancellationToken cancellationToken)
    {
        using (_databaseConnectionInfo)
        {
            return _databaseConnectionInfo.QueryAsync(
                _ => _.CountryCode == countryCode && (_.DisplayContent?.ContainsKey(languageCode) ?? false),
                maxPromotions,
                cancellationToken);
        }
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
