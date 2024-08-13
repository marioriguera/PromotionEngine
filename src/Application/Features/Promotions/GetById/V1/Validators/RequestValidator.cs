using FluentValidation;

namespace PromotionEngine.Application.Features.Promotions.GetById.V1.Validators;

/// <summary>
/// Validator for <see cref="PromotionByIdV1Request"/> to ensure that its properties meet the required validation rules.
/// </summary>
/// <remarks>
/// This class inherits from <see cref="AbstractValidator{T}"/> and defines validation rules for
/// the <see cref="PromotionByIdV1Request"/> to ensure that the provided GUID is valid and not part of a predefined set of disallowed GUIDs.
/// </remarks>
public sealed class PromotionByIdV1RequestValidator : AbstractValidator<PromotionByIdV1Request>
{
    private readonly HashSet<Guid> _notAllowedGuids = new HashSet<Guid>
    {
        Guid.Parse("00000000-0000-0000-0000-000000000000")
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="PromotionByIdV1RequestValidator"/> class.
    /// Defines validation rules for the <see cref="PromotionByIdV1Request"/>.
    /// </summary>
    public PromotionByIdV1RequestValidator()
    {
        RuleFor(x => x.PromotionId)
            .NotEmpty()
            .WithMessage("The promotion id cannot be empty.");

        RuleFor(x => x.PromotionId)
            .Must(guid => !_notAllowedGuids.Contains(guid))
            .WithMessage(pr => $"The GUID is not allowed. GUID: {pr.PromotionId} .");
    }
}