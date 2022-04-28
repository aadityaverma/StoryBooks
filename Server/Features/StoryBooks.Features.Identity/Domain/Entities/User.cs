namespace StoryBooks.Features.Identity.Domain.Entities;

using Microsoft.AspNetCore.Identity;

using StoryBooks.Features.Domain.Entities;
using StoryBooks.Features.Domain.Interfaces;
using StoryBooks.Features.Identity.Domain.Exceptions;
using StoryBooks.Libraries.Validation;

using static StoryBooks.Features.Identity.Domain.IdentityDomainConstants;

public class User : IdentityUser, IAggregateRoot, IBaseUser
{
    internal User(
        string email, 
        string firstName, 
        string lastName) : base(email)
    {
        this.SetFirstName(firstName);
        this.SetLastName(lastName);

        this.Gender = default!;
    }

    public string FirstName { get; private set; } = default!;

    public string LastName { get; private set; } = default!;

    public Gender? Gender { get; private set; }

    public DateTime? DateOfBirth { get; private set; }

    public int Age 
    {
        get 
        {
            if (this.DateOfBirth is not null)
            {
                return this.DateOfBirth.Value.UtcAge();
            }

            return default;
        }
    }

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

    public User SetGender(Gender? gender)
    {
        this.Gender = gender;
        return this;
    }

    public User SetDateOfBirth(DateOnly? date)
    {
        if (date == null)
        {
            this.DateOfBirth = null;
            return this;
        }

        this.DateOfBirth = date.Value.ToDateTime(TimeOnly.MinValue);
        return this;
    }

    private static void ValidateName(string name) 
        => Guard.ForStringLength<InvalidUserNameException>(
            name, Validation.MinNameLength, Validation.MaxNameLength, nameof(name));
}