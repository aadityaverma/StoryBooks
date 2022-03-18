namespace StoryBooks.Features.Common.Domain.Entities;

using StoryBooks.Features.Common.Domain.Exceptions;
using StoryBooks.Libraries.Validation;

using static StoryBooks.Libraries.Validation.CommonValidationConstants;

public class PhoneNumber : ValueObject
{
    public PhoneNumber(string number)
    {
        this.ValidateModel(number);
        this.Number = number;
    }

    public string Number { get; }

    public static implicit operator string(PhoneNumber number) => number.Number;

    public static implicit operator PhoneNumber(string number) => new(number);

    private void ValidateModel(string phoneNumber)
    {
        Guard.ForStringLength<InvalidPhoneNumberException>(
            phoneNumber,
            Phone.MinLength,
            Phone.MaxLength,
            nameof(PhoneNumber));

        Guard.ForValidFormat<InvalidPhoneNumberException>(
            phoneNumber,
            Phone.RegularExpression,
            Phone.FormatErrorMessage,
            nameof(PhoneNumber));
    }
}