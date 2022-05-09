namespace StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;

using MediatR;

using Microsoft.AspNetCore.Identity;

using StoryBooks.Features.Application;
using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;

using System.Linq;
using System.Threading.Tasks;
using System.Web;

using static StoryBooks.Features.Identity.Application.IdentityApplicationConstants;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, RedirectResult>
{
    private readonly UserManager<User> userManager;
    private readonly IIdentityUrlProvider urlProvider;

    public ConfirmEmailCommandHandler(
        UserManager<User> userManager,
        IIdentityUrlProvider urlProvider)
    {
        this.userManager = userManager;
        this.urlProvider = urlProvider;
    }

    public async Task<RedirectResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            return new(this.urlProvider.ClientNotFoundUrl());
        }

        string? token = HttpUtility.UrlDecode(request.Token);
        var identityResult = await this.userManager.ConfirmEmailAsync(user, token);
        var errors = identityResult.Errors.Select(e => new ResultError(e.Code, e.Description));

        return identityResult.Succeeded ?
            new(this.urlProvider.ConfirmEmailRedirectLink(Messages.EmailConfirmed)) :
            new(this.urlProvider.ClientErrorUrl(Messages.EmailConfirmError));
    }
}