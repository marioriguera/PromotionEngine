﻿using System.Runtime.CompilerServices;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Shared;

/// <summary>
/// DISCLAIMER: YOU CAN'T MODIFY THIS FILE, THIS IS BEEING USED TO SIMULATE A DATABASE
/// </summary>
/// Represents a connection to the database for querying promotions.
internal sealed class DatabaseConnection
    : IDisposable
{
    private readonly ILogger<DatabaseConnection> _logger;
    private readonly string _connectionString;
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
    /// Initializes a new instance of the <see cref="DatabaseConnection"/> class with the specified connection string and logger.
    /// </summary>
    /// <param name="connectionString">The connection string used to establish the database connection.</param>
    /// <param name="logger">The logger used to record database connection activities and errors.</param>
    public DatabaseConnection(string connectionString, ILogger<DatabaseConnection> logger)
    {
        _logger = logger;
        _connectionString = connectionString;
    }

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
    public async Task<bool> ConnectAsync(CancellationToken cancellationToken) => await Task.FromResult(!string.IsNullOrEmpty(_connectionString));

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
        // Dispose resources if needed.
        // I will not delete the data from the list of promotions in memory.
        _logger.LogInformation($"Database connection resources will be dispose.");
    }
}
