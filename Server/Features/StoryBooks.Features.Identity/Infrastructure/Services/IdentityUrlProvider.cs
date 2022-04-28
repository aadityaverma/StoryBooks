namespace StoryBooks.Features.Identity.Infrastructure.Services;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

using StoryBooks.Features.Application;
using StoryBooks.Features.Identity.Application.Services;

internal class IdentityUrlProvider : IIdentityUrlProvider
{
    private const string ConfirmEmailEndpointName = "AccountManageEmail";

    private readonly ApplicationSettings settings;
    private readonly LinkGenerator linker;

    public IdentityUrlProvider(
        IOptions<ApplicationSettings> opts,
        LinkGenerator linker)
    {
        this.settings = opts.Value;
        this.linker = linker;
    }

    public string ClientUrl => this.settings.URLs.ClientUrl;

    public string CoreApiUrl => this.settings.URLs.CoreApiUrl;

    public string ConfirmEmailLink(string userId, string token)
        => $"{this.CoreApiUrl}{this.linker.GetPathByName(ConfirmEmailEndpointName, values: new { userId, token })}";

    #region Client URLs
    public string ClientErrorUrl(string message)
        => $"{this.ClientUrl}/error?message={message}";

    public string ConfirmEmailRedirectLink(string message) 
        => $"{this.ClientUrl}/profile?message={message}";

    public string ClientNotFoundUrl()
         => $"{this.ClientUrl}/not-found";
    #endregion
}