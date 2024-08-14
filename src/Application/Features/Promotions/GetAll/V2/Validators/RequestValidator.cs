using FluentValidation;
using PromotionEngine.Application.Features.Promotions.GetAll.V1.Validators;
using PromotionEngine.Application.Features.Promotions.GetAll.V1;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2.Validators;

/// <summary>
/// Validator for <see cref="GetAllPromotionsV2Request"/> to ensure that its properties meet the required validation rules.
/// </summary>
/// <remarks>
/// This class inherits from <see cref="AbstractValidator{T}"/> and defines validation rules for the properties of
/// <see cref="GetAllPromotionsV2Request"/>. It ensures that the country code and language code are properly formatted and
/// within acceptable length ranges, and that the number of promotions requested is within specified limits.
/// </remarks>
public sealed class AllPromotionsV2RequestValidator : AbstractValidator<GetAllPromotionsV2Request>
{
    private readonly int _minCountryLanguageCodeLength = 2;
    private readonly int _maxCountryLanguageCodeLength = 3;
    private readonly int _minCountPromotions = 1;
    private readonly int _maxCountPromotions = 50;
    private readonly string _countryCodeRegex = @"^[^\d]*$";

    /// <summary>
    /// Initializes a new instance of the <see cref="AllPromotionsV2RequestValidator"/> class.
    /// Defines validation rules for the properties of <see cref="GetAllPromotionsV2Request"/>.
    /// </summary>
    public AllPromotionsV2RequestValidator()
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

        RuleFor(x => x.MaxPromotions)
            .GreaterThanOrEqualTo(_minCountPromotions)
            .WithMessage($"You must apply for at least one promotion.");

        RuleFor(x => x.MaxPromotions)
            .LessThanOrEqualTo(_maxCountPromotions)
            .WithMessage($"You can apply for a maximum of {_maxCountPromotions} promotions.");
    }
}