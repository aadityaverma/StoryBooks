namespace StoryBooks.Features.Common.Domain.Entities
{
    using StoryBooks.Features.Common.Domain.Exceptions;
    using StoryBooks.Libraries.Validation;

    using System.Text.RegularExpressions;

    using static StoryBooks.Libraries.Validation.CommonValidationConstants;

    public class PhoneNumber : ValueObject
    {
        internal PhoneNumber(string number)
        {
            this.ValidateModel(number);

            if (!Regex.IsMatch(number, Phone.RegularExpression))
            {
                throw new InvalidPhoneNumberException(Phone.FormatErrorMessage);
            }

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
        }
    }
}
