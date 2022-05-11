namespace StoryBooks.Features.Identity.Application.Commands.ResendConfirmEmail;

using Microsoft.AspNetCore.Identity;

using StoryBooks.Features.Application.Interfaces;
using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;

using System.Web;

using static StoryBooks.Features.Identity.Application.IdentityApplicationConstants;

public class ResendConfirmEmailCommandHandler : IRequestHandler<ResendConfirmEmailCommand, Result>
{
    private readonly UserManager<User> userManager;
    private readonly ICurrentUser currentUser;
    private readonly IIdentityEmailService emailService;

    public ResendConfirmEmailCommandHandler(
        UserManager<User> userManager,
        ICurrentUser currentUser,
        IIdentityEmailService emailService)
    {
        this.userManager = userManager;
        this.currentUser = currentUser;
        this.emailService = emailService;
    }

    public async Task<Result> Handle(ResendConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByNameAsync(this.currentUser.Email);
        if (user is null)
        {
            return Result.NotFound(Messages.InvalidLoginError);
        }

        string? token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
        string? urlToken = HttpUtility.UrlEncode(token);
        await this.emailService.SendConfirmEmail(user, urlToken);

        return Result.Success(Messages.EmailConfirmSent);
    }
}