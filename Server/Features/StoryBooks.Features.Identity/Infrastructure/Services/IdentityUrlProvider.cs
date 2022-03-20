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

    public string ClientUrl => settings.URLs.ClientUrl;

    public string CoreApiUrl => settings.URLs.CoreApiUrl;

    public string ConfirmEmailLink(string userId, string token)
        => PrependServerUrl(
            linker.GetPathByName(ConfirmEmailEndpointName, values: new { userId, token }));

    private string PrependServerUrl(string? path)
    {
        return $"{CoreApiUrl}{path}";
    }
}