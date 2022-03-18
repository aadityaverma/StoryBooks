namespace StoryBooks.Features.Identity.Domain.Factories;

using StoryBooks.Features.Domain.Interfaces;
using StoryBooks.Features.Identity.Domain.Entities;

public interface IUserFactory : IFactory<User>
{
    IUserFactory AddFirstName(string name);

    IUserFactory AddLastName(string name);

    IUserFactory AddEmail(string email);
}