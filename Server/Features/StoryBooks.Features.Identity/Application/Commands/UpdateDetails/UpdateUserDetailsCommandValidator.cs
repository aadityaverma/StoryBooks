namespace StoryBooks.Features.Identity.Application.Commands.UpdateDetails
{
    using FluentValidation;

    using static StoryBooks.Features.Identity.Domain.IdentityDomainConstants;
    using static StoryBooks.Libraries.Validation.CommonValidationConstants;

    public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
    {
        public UpdateUserDetailsCommandValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty()
                .MinimumLength(Validation.MinNameLength)
                .MaximumLength(Validation.MaxNameLength);

            RuleFor(m => m.LastName)
                .NotEmpty()
                .MinimumLength(Validation.MinNameLength)
                .MaximumLength(Validation.MaxNameLength);

            RuleFor(m => m.PhoneNumber)
                .Matches(Phone.RegularExpression)
                .WithMessage(Phone.FormatErrorMessage)
                .MinimumLength(Phone.MinLength)
                .MaximumLength(Phone.MaxLength);
        }
    }
}
