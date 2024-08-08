using System.Runtime.CompilerServices;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared;

/// <summary>
/// DISCLAIMER: YOU CAN'T MODIFY THIS FILE, THIS IS BEEING USED TO SIMULATE A DATABASE
/// </summary>
/// Represents a connection to the database for querying promotions.
/// <param name="connectionString">The connection string for the database.</param>
internal sealed class DatabaseConnection(string connectionString)
    : IDisposable
{
    private readonly List<Promotion> _promotions =
    [
            new Promotion()
            {
                Id = Guid.NewGuid(),
                CountryCode = "ES",
                CreatedDate = DateTime.Now,
                Images = [ "Image1", "Image2"],
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Enabled,
                EndValidityDate = DateTime.Now.AddDays(1),

                DisplayContent = new Dictionary<string, DisplayContent>()
                {
                    {
                        "ES",
                        new DisplayContent()
                        {
                            Description = "Description",
                            DiscountDescription = "Discount Description",
                            DiscountTitle = "Discount Title",
                            Title = "Title"
                        }
                    },
                    {
                        "EN",
                        new DisplayContent()
                        {
                            Description = "Description",
                            DiscountDescription = "Discount Description",
                            DiscountTitle = "Discount Title",
                            Title = "Title"
                        }
                    }
                },
                Discounts =
                [
                    new StoreDiscount()
                    {
                        FinalPrice = 1,
                        HasPrice = true,
                        LowestPriceLast30Days = 1,
                        OriginalPrice = 1,
                        PriceType = "Type1",
                        UnitsToBuy = 1,
                        UnitsToPay = 1
                    }

                ]
            },
            new Promotion()
            {
                Id = Guid.NewGuid(),
                CountryCode = "DE",
                CreatedDate = DateTime.Now,
                Images = ["Image3", "Image4"],
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Enabled,
                EndValidityDate = DateTime.Now.AddDays(2),
                DisplayContent = new Dictionary<string, DisplayContent>()
                {
                    {
                        "DE",
                        new DisplayContent()
                        {
                            Description = "Description",
                            DiscountDescription = "Discount Description",
                            DiscountTitle = "Discount Title",
                            Title = "Title"
                        }
                    }
                },
                Discounts =
                [
                    new StoreDiscount()
                    {
                        FinalPrice = 1,
                        HasPrice = true,
                        LowestPriceLast30Days = 1,
                        OriginalPrice = 1,
                        PriceType = "Type1",
                        UnitsToBuy = 1,
                        UnitsToPay = 1
                    }

                ]
            },
            new Promotion()
            {
                Id = Guid.NewGuid(),
                CountryCode = "DE",
                CreatedDate = DateTime.Now,
                Images = ["Image3", "Image4"],
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Enabled,
                EndValidityDate = DateTime.Now.AddDays(2),
                DisplayContent = new Dictionary<string, DisplayContent>()
                {
                    {
                        "DE",
                        new DisplayContent()
                        {
                            Description = "Description",
                            DiscountDescription = "Discount Description",
                            DiscountTitle = "Discount Title",
                            Title = "Title"
                        }
                    }
                },
                Discounts =
                [
                    new StoreDiscount()
                    {
                        FinalPrice = 1,
                        HasPrice = true,
                        LowestPriceLast30Days = 1,
                        OriginalPrice = 1,
                        PriceType = "Type1",
                        UnitsToBuy = 1,
                        UnitsToPay = 1
                    },
                    new OnlineDiscount()
                    {
                        FinalPrice = 1,
                        HasPrice = true,
                        LowestPriceLast30Days = 1,
                        OriginalPrice = 1,
                        PriceType = "Type1",
                        UnitsToBuy = 1,
                        UnitsToPay = 1
                    }

                ]
            },
            new Promotion()
            {
                Id = Guid.NewGuid(),
                CountryCode = "DE",
                CreatedDate = DateTime.Now,
                Images = ["Image3", "Image4"],
                LastModifiedDate = DateTime.Now,
                Status = PromotionStatus.Disabled,
                EndValidityDate = DateTime.Now.AddDays(2),
                DisplayContent = new Dictionary<string, DisplayContent>()
                {
                    {
                        "DE",
                        new DisplayContent()
                        {
                        Description = "Description",
                        DiscountDescription = "Discount Description",
                        DiscountTitle = "Discount Title",
                        Title = "Title"
                        }
                    }
                },
                Discounts =
                [
                    new StoreDiscount()
                    {
                        FinalPrice = 1,
                        HasPrice = true,
                        LowestPriceLast30Days = 1,
                        OriginalPrice = 1,
                        PriceType = "Type1",
                        UnitsToBuy = 1,
                        UnitsToPay = 1
                    },
                    new OnlineDiscount()
                    {
                        FinalPrice = 1,
                        HasPrice = true,
                        LowestPriceLast30Days = 1,
                        OriginalPrice = 1,
                        PriceType = "Type1",
                        UnitsToBuy = 1,
                        UnitsToPay = 1
                    }

                ]
            }

        ];

    /// <summary>
    /// Queries the database asynchronously for promotions that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter the promotions.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>An async enumerable of promotions matching the predicate.</returns>
    public async IAsyncEnumerable<Promotion> QueryAsync(Func<Promotion, bool> predicate, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (!await ConnectAsync(cancellationToken))
        {
            throw new InvalidOperationException("Failed to connect to the database.");
        }

        foreach (var promotion in _promotions.Where(predicate))
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            await Task.Yield();
            yield return promotion;
        }
    }

    /// <summary>
    /// Connects to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<bool> ConnectAsync(CancellationToken cancellationToken) => await Task.FromResult(!string.IsNullOrEmpty(connectionString));

    /// <summary>
    /// Pings the database to check the connection.
    /// </summary>
    /// <returns>A task representing the asynchronous operation, with a boolean result indicating success.</returns>
    public async Task<bool> PingAsync() => await Task.FromResult(true);

    /// <summary>
    /// Releases all resources used by the <see cref="DatabaseConnection"/>.
    /// </summary>
    public void Dispose()
    {
        // Dispose resources if needed (example purposes)
    }
}
