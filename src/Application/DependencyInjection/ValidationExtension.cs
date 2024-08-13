using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PromotionEngine.Application.Shared.Constants;

namespace PromotionEngine.Application.DependencyInjection;

/// <summary>
/// Provides extension methods for validation operations.
/// </summary>
/// <remarks>
/// This static class contains extension methods that simplify the process of validating objects
/// using a validator and handling the results of those validations. It is intended to be used
/// with implementations of the <see cref="IValidator{T}"/> interface from the FluentValidation library.
/// </remarks>
public static class ValidationExtension
{
    /// <summary>
    /// Validates the given request using the specified validator and returns a list of validation errors if the validation fails.
    /// </summary>
    /// <typeparam name="T">The type of the request object being validated.</typeparam>
    /// <param name="validator">The validator to use for validating the request.</param>
    /// <param name="request">The request object to be validated.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the validation operation if needed.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a list of <see cref="Error"/> objects if validation fails,
    /// or <c>null</c> if validation succeeds.
    /// </returns>
    public static async Task<List<Error>?> ExecuteValidateAsync<T>(this IValidator<T> validator, T request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .ConvertAll(validationFailure => Error.Validation(
                    validationFailure.PropertyName,
                    validationFailure.ErrorMessage));
            return errors;
        }

        return null;
    }
}