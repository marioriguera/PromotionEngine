using System.Diagnostics;
using System.Net;

namespace PromotionEngine.Application.Shared;

/// <summary>
/// Base class for API controllers providing common functionalities for error handling and logging.
/// </summary>
public abstract class FeatureControllerBase : ControllerBase
{
    /// <summary>
    /// The logger instance used for logging errors and other information.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="FeatureControllerBase"/> class.
    /// </summary>
    /// <param name="logger">The logger instance used for logging errors and other information.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> is null.</exception>
    public FeatureControllerBase(ILogger logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Handles exceptions by creating a problem details response with a status code of 500 (Internal Server Error).
    /// </summary>
    /// <param name="ex">The exception to handle.</param>
    /// <returns>An <see cref="IActionResult"/> representing the problem details response.</returns>
    protected IActionResult HandleException(Exception ex)
    {
        return Problem(ex, (int)HttpStatusCode.InternalServerError, LogLevel.Error);
    }

    /// <summary>
    /// Creates a problem details response with a specified status code and logs the exception.
    /// </summary>
    /// <param name="ex">The exception to include in the problem details.</param>
    /// <param name="statusCode">The HTTP status code for the response.</param>
    /// <param name="logLevel">The log level used for logging the exception.</param>
    /// <returns>An <see cref="IActionResult"/> representing the problem details response.</returns>
    protected IActionResult Problem(Exception ex, int statusCode, LogLevel logLevel = LogLevel.Information)
    {
        if (logLevel != LogLevel.None)
        {
            _logger.Log(logLevel: logLevel, exception: ex, message: ex.Message);
        }

        var problem = new ProblemDetails()
        {
            Type = $"https://httpstatuses.com/{statusCode}",
            Title = ex.Message,
#if DEBUG
            Detail = ex.ToString(),
#endif
            Status = statusCode,
            Instance = Request.Path,
            Extensions = { { "traceId", Activity.Current?.TraceId.ToString() } }
        };

        return StatusCode(problem.Status.Value, problem);
    }

    /// <summary>
    /// Creates a problem details response for validation errors and logs the issue.
    /// </summary>
    /// <param name="errors">A dictionary containing validation errors.</param>
    /// <param name="statusCode">The HTTP status code for the response (default is 400 Bad Request).</param>
    /// <returns>An <see cref="ObjectResult"/> representing the validation problem details response.</returns>
    protected ObjectResult Problem(
        IDictionary<string, string[]> errors,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        var problem = new ValidationProblemDetails(errors)
        {
            Type = $"https://httpstatuses.com/{(int)statusCode}",
            Title = "One or more validation errors occurred.",
            Status = (int)statusCode,
            Instance = Request?.Path,
            Extensions = { { "traceId", Activity.Current?.TraceId.ToString() } }
        };

        return StatusCode(problem.Status.Value, problem);
    }
}