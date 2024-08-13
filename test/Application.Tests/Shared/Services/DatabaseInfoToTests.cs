using System.Runtime.CompilerServices;
using PromotionEngine.Application.Shared.Interfaces;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared.Services;

/// <summary>
/// DISCLAIMER: YOU CAN'T MODIFY THIS FILE, THIS IS BEEING USED TO SIMULATE A DATABASE
/// </summary>
/// Represents a connection to the database for querying promotions.
internal sealed class DatabaseInfoToTests : IDatabaseConnection
{
    private readonly List<Promotion> _promotions =
    [
        new Promotion()
        {
            Id = new Guid("7d9f9c1b-911f-4b6f-9442-716f0e2f5f8c"),
            CountryCode = "ES",
            CreatedDate = DateTime.Now.AddDays(-10),
            Images = new List<string> { "Image1", "Image2" },
            LastModifiedDate = DateTime.Now.AddDays(-2),
            Status = PromotionStatus.Enabled,
            EndValidityDate = DateTime.Now.AddDays(5),

            DisplayContent = new Dictionary<string, DisplayContent>()
            {
                {
                    "ES",
                    new DisplayContent()
                    {
                        Description = "Spring Sale",
                        DiscountDescription = "10% off on all items",
                        DiscountTitle = "Spring Discount",
                        Title = "Welcome Spring"
                    }
                },
                {
                    "EN",
                    new DisplayContent()
                    {
                        Description = "Spring Sale",
                        DiscountDescription = "10% off on all items",
                        DiscountTitle = "Spring Discount",
                        Title = "Welcome Spring"
                    }
                }
            },
            Discounts = new List<Discount>()
            {
                new StoreDiscount()
                {
                    FinalPrice = 9.99M,
                    HasPrice = true,
                    LowestPriceLast30Days = 12.99M,
                    OriginalPrice = 15.99M,
                    PriceType = "Standard",
                    UnitsToBuy = 2,
                    UnitsToPay = 1
                }
            }
        },
        new Promotion()
        {
            Id = new Guid("1b5f7a3e-ef5d-47c8-8720-233d41fa6eb8"),
            CountryCode = "DE",
            CreatedDate = DateTime.Now.AddDays(-30),
            Images = new List<string> { "Image3", "Image4" },
            LastModifiedDate = DateTime.Now.AddDays(-20),
            Status = PromotionStatus.Enabled,
            EndValidityDate = DateTime.Now.AddDays(15),

            DisplayContent = new Dictionary<string, DisplayContent>()
            {
                {
                    "DE",
                    new DisplayContent()
                    {
                        Description = "Summer Special",
                        DiscountDescription = "15% off on selected items",
                        DiscountTitle = "Summer Discount",
                        Title = "Enjoy the Summer"
                    }
                }
            },
            Discounts = new List<Discount>()
            {
                new StoreDiscount()
                {
                    FinalPrice = 19.99M,
                    HasPrice = true,
                    LowestPriceLast30Days = 24.99M,
                    OriginalPrice = 29.99M,
                    PriceType = "Sale",
                    UnitsToBuy = 1,
                    UnitsToPay = 1
                }
            }
        },
        new Promotion()
        {
            Id = new Guid("a93f3c56-4c5b-4e7a-a1cb-ea29c7fa6d6e"),
            CountryCode = "DE",
            CreatedDate = DateTime.Now.AddDays(-40),
            Images = new List<string> { "Image5", "Image6" },
            LastModifiedDate = DateTime.Now.AddDays(-15),
            Status = PromotionStatus.Enabled,
            EndValidityDate = DateTime.Now.AddDays(20),

            DisplayContent = new Dictionary<string, DisplayContent>()
            {
                {
                    "DE",
                    new DisplayContent()
                    {
                        Description = "Autumn Deals",
                        DiscountDescription = "20% off on all outdoor gear",
                        DiscountTitle = "Autumn Discount",
                        Title = "Gear Up for Autumn"
                    }
                }
            },
            Discounts = new List<Discount>()
            {
                new StoreDiscount()
                {
                    FinalPrice = 29.99M,
                    HasPrice = true,
                    LowestPriceLast30Days = 39.99M,
                    OriginalPrice = 49.99M,
                    PriceType = "Clearance",
                    UnitsToBuy = 3,
                    UnitsToPay = 2
                },
                new OnlineDiscount()
                {
                    FinalPrice = 27.99M,
                    HasPrice = true,
                    LowestPriceLast30Days = 35.99M,
                    OriginalPrice = 45.99M,
                    PriceType = "LimitedTime",
                    UnitsToBuy = 2,
                    UnitsToPay = 1
                }
            }
        },
        new Promotion()
        {
            Id = new Guid("5f1c2c74-fb47-4d3e-9025-18b7d2c36ec6"),
            CountryCode = "DE",
            CreatedDate = DateTime.Now.AddDays(-50),
            Images = new List<string> { "Image7", "Image8" },
            LastModifiedDate = DateTime.Now.AddDays(-10),
            Status = PromotionStatus.Disabled,
            EndValidityDate = DateTime.Now.AddDays(25),

            DisplayContent = new Dictionary<string, DisplayContent>()
            {
                {
                    "DE",
                    new DisplayContent()
                    {
                        Description = "Winter Clearance",
                        DiscountDescription = "50% off on all winter clothing",
                        DiscountTitle = "Winter Sale",
                        Title = "Winter Blowout"
                    }
                }
            },
            Discounts = new List<Discount>()
            {
                new StoreDiscount()
                {
                    FinalPrice = 49.99M,
                    HasPrice = true,
                    LowestPriceLast30Days = 59.99M,
                    OriginalPrice = 69.99M,
                    PriceType = "Clearance",
                    UnitsToBuy = 4,
                    UnitsToPay = 3
                },
                new OnlineDiscount()
                {
                    FinalPrice = 47.99M,
                    HasPrice = true,
                    LowestPriceLast30Days = 57.99M,
                    OriginalPrice = 67.99M,
                    PriceType = "OnlineExclusive",
                    UnitsToBuy = 3,
                    UnitsToPay = 2
                }
            }
        },
        new Promotion()
        {
            Id = new Guid("684d2064-916f-41bb-8ff9-23c83c7f9282"),
            CountryCode = "CU",
            CreatedDate = DateTime.Parse("2024-08-08T19:31:06.6821825+02:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
            Images = new List<string> { "Image9", "Image10" },
            LastModifiedDate = DateTime.Parse("2024-08-08T19:32:46.6392642+02:00", null, System.Globalization.DateTimeStyles.RoundtripKind),
            Status = PromotionStatus.Disabled,
            EndValidityDate = DateTime.Parse("2024-08-08T19:33:35.9697746+02:00", null, System.Globalization.DateTimeStyles.RoundtripKind),

            DisplayContent = new Dictionary<string, DisplayContent>()
            {
                {
                    "CU",
                    new DisplayContent()
                    {
                        Description = "Caribbean Getaway",
                        DiscountDescription = "20% off on all travel packages",
                        DiscountTitle = "Cuba Discount",
                        Title = "Escape to Cuba"
                    }
                }
            },
            Discounts = new List<Discount>()
            {
                new StoreDiscount()
                {
                    FinalPrice = 99.99M,
                    HasPrice = true,
                    LowestPriceLast30Days = 119.99M,
                    OriginalPrice = 149.99M,
                    PriceType = "TravelDeal",
                    UnitsToBuy = 1,
                    UnitsToPay = 1
                },
                new OnlineDiscount()
                {
                    FinalPrice = 95.99M,
                    HasPrice = true,
                    LowestPriceLast30Days = 115.99M,
                    OriginalPrice = 145.99M,
                    PriceType = "LimitedTime",
                    UnitsToBuy = 2,
                    UnitsToPay = 1
                }
            }
        }

    ];

    /// <inheritdoc/>
    public IAsyncEnumerable<Promotion> QueryAsync(Func<Promotion, bool> predicate, CancellationToken cancellationToken)
    {
        return QueryAsyncInternal(predicate, null, cancellationToken);
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<Promotion> QueryAsync(Func<Promotion, bool> predicate, int countMaxToTake, CancellationToken cancellationToken)
    {
        return QueryAsyncInternal(predicate, countMaxToTake, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> ConnectAsync(CancellationToken cancellationToken) => await Task.FromResult(true);

    /// <inheritdoc/>
    public async Task<bool> PingAsync() => await Task.FromResult(true);

    /// <summary>
    /// Releases all resources used by the <see cref="DatabaseConnection"/>.
    /// </summary>
    public void Dispose()
    {
        // Dispose resources if needed.
        // I will not delete the data from the list of promotions in memory.
        System.Console.WriteLine($"Database connection resources will be dispose.");
    }

    /// <summary>
    /// Queries the in-memory promotions collection asynchronously based on the specified predicate and maximum count.
    /// </summary>
    /// <param name="predicate">The predicate to filter the promotions.</param>
    /// <param name="countMaxToTake">The maximum number of promotions to retrieve, or null to retrieve all matching promotions.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>An async enumerable of promotions matching the predicate, up to the specified count.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the database connection fails.</exception>
    private async IAsyncEnumerable<Promotion> QueryAsyncInternal(
        Func<Promotion, bool> predicate,
        int? countMaxToTake,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        if (!await ConnectAsync(cancellationToken))
        {
            throw new InvalidOperationException("Failed to connect to the database.");
        }

        int count = 0;

        foreach (var promotion in _promotions.Where(predicate))
        {
            if (cancellationToken.IsCancellationRequested || (countMaxToTake.HasValue && count >= countMaxToTake.Value))
                break;

            await Task.Yield();
            yield return promotion;

            count++;
        }
    }
}
