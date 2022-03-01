namespace StoryBooks.Features.Identity.Application.Services
{
    using StoryBooks.Features.Identity.Domain.Entities;

    public interface IIdentityEmailService
    {
        Task SendUserRegisteredEmail(User user);
    }
}