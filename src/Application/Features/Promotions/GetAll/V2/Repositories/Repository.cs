using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2.Repositories;

/// <summary>
/// Repository for retrieving version 2 promotions from the database.
/// </summary>
internal sealed class PromotionsV2Repository : IPromotionsV2Repository
{
    private readonly IDatabaseConnection _databaseConnectionInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionsV2Repository"/> class.
    /// </summary>
    /// <param name="databaseConnectionInfo">The database connection information.</param>
    public PromotionsV2Repository(IDatabaseConnection databaseConnectionInfo)
    {
        _databaseConnectionInfo = databaseConnectionInfo;
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
}
