using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PromotionEngine.Application.Shared;

/// <summary>
/// Base class for API controllers providing common functionalities for error handling and logging.
/// </summary>
[ApiController]
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
    /// Handles different types of errors and returns an appropriate HTTP response.
    /// </summary>
    /// <param name="errors">The list of errors to handle.</param>
    /// <returns>An IActionResult representing the HTTP response.</returns>
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Erros] = errors;

        return Problem(errors[0]);
    }

    /// <summary>
    /// Returns an appropriate HTTP response based on a single error.
    /// </summary>
    /// <param name="error">The error to handle.</param>
    /// <returns>An IActionResult representing the HTTP response.</returns>
    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }

    /// <summary>
    /// Returns a validation error response with a list of validation errors.
    /// </summary>
    /// <param name="errors">The list of validation errors.</param>
    /// <returns>An IActionResult representing the HTTP response.</returns>
    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }
}