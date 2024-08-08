using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace PromotionEngine.Middlewares;

/// <summary>
/// Middleware for handling global exceptions and returning a standardized error response.
/// </summary>
public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="logger">The logger used to log errors.</param>
    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) => _logger = logger;

    /// <summary>
    /// Invokes the middleware to handle exceptions.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal server error has occurred.",
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}
