namespace StoryBooks.Features.Identity.Application.Services;

using StoryBooks.Features.Application.Services;

public interface IIdentityUrlProvider : IUrlProvider
{
    string ConfirmEmailLink(string userId, string code);

    string ConfirmEmailRedirectLink(string message);

    string ClientErrorUrl(string message);

    string ClientNotFoundUrl();
}