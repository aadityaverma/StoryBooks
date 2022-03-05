namespace StoryBooks.Features.Identity.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;

    using StoryBooks.Features.Common.Domain.Entities;
    using StoryBooks.Features.Common.Domain.Interfaces;
    using StoryBooks.Features.Identity.Domain.Exceptions;
    using StoryBooks.Libraries.Validation;

    using static StoryBooks.Features.Identity.Domain.IdentityDomainConstants;

    public class User : IdentityUser, IAggregateRoot, IBaseUser
    {
        internal User(string email, string firstName, string lastName) : base (email)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
        }

        public string FirstName { get; private set; } = default!;

        public string LastName { get; private set; } = default!;

        public User SetFirstName(string firstName)
        {
            ValidateName(firstName);
            this.FirstName = firstName;
            return this;
        }

        public User SetLastName(string lastName)
        {
            ValidateName(lastName);
            this.LastName = lastName;
            return this;
        }

        public User SetPhoneNumber(string? phoneNumber)
        {
            if (phoneNumber is null)
            {
                this.PhoneNumber = null;
                return this;
            }

            this.PhoneNumber = new PhoneNumber(phoneNumber);
            return this;
        }

        private static void ValidateName(string name)
        {
            Guard.ForStringLength<InvalidUserNameException>(
                name, Validation.MinNameLength, Validation.MaxNameLength, nameof(name));
        }
    }
}
