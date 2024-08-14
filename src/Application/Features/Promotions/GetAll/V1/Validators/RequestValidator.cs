using FluentValidation;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1.Validators;

/// <summary>
/// Validator for the <see cref="GetAllPromotionsV1Request"/> class, ensuring that the request parameters are valid.
/// </summary>
public sealed class AllPromotionsV1RequestValidator : AbstractValidator<GetAllPromotionsV1Request>
{
    private readonly int _minCountryLanguageCodeLength = 2;
    private readonly int _maxCountryLanguageCodeLength = 3;
    private readonly string _countryCodeRegex = @"^[^\d]*$";

    /// <summary>
    /// Initializes a new instance of the <see cref="AllPromotionsV1RequestValidator"/> class.
    /// Defines validation rules for the properties of <see cref="GetAllPromotionsV1Request"/>.
    /// </summary>
    public AllPromotionsV1RequestValidator()
    {
        RuleFor(x => x.CountryCode)
            .NotEmpty()
            .WithMessage("The country code cannot be empty.");

        RuleFor(x => x.CountryCode)
            .Length(_minCountryLanguageCodeLength, _maxCountryLanguageCodeLength)
            .WithMessage($"The country code must have a minimum of {_minCountryLanguageCodeLength} characters and a maximum of {_maxCountryLanguageCodeLength}.");

        RuleFor(x => x.CountryCode)
            .Matches(_countryCodeRegex)
            .WithMessage("The country code must not contain numeric characters.");

        RuleFor(x => x.LanguageCode)
            .NotEmpty()
            .WithMessage("The language code cannot be empty.");

        RuleFor(x => x.LanguageCode)
            .Length(_minCountryLanguageCodeLength, _maxCountryLanguageCodeLength)
            .WithMessage($"The language code must have a minimum of {_minCountryLanguageCodeLength} characters and a maximum of {_maxCountryLanguageCodeLength}.");

        RuleFor(x => x.LanguageCode)
            .Matches(_countryCodeRegex)
            .WithMessage("The language code must not contain numeric characters.");
    }
}