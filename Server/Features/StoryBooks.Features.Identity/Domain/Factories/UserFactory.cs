namespace StoryBooks.Features.Identity.Domain.Factories;

using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Domain.Exceptions;

using static StoryBooks.Features.Identity.Domain.IdentityDomainConstants;

public class UserFactory : IUserFactory
{
    private string email = default!;
    private string firstName = default!;
    private string lastName = default!;

    private bool emailSet = false;
    private bool firstNameSet = false;
    private bool lastNameSet = false;

    public IUserFactory AddEmail(string email)
    {
        this.email = email;
        this.emailSet = true;
        return this;
    }

    public IUserFactory AddFirstName(string name)
    {
        this.firstName = name;
        this.firstNameSet = true;
        return this;
    }

    public IUserFactory AddLastName(string name)
    {
        this.lastName = name;
        this.lastNameSet = true;
        return this;
    }

    public User Build()
    {
        if (!this.emailSet || !this.firstNameSet || !this.lastNameSet)
        {
            throw new InvalidIdentityUserException(ErrorMessages.InvalidUserFields);
        }

        return new User(email, firstName, lastName);
    }

    public void Reset()
    {
        this.emailSet = false;
        this.firstNameSet = false;
        this.lastNameSet = false;
    }
}