namespace StoryBooks.Features.Identity.Application.Commands.BecomeAuthor;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using StoryBooks.Features.Application.Interfaces;
using StoryBooks.Features.Application.Messages;
using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Domain.Exceptions;
using StoryBooks.Libraries.EventBus.Services;
using StoryBooks.Libraries.Validation;

using static StoryBooks.Features.Identity.Application.IdentityApplicationConstants;

internal class BecomeAuthorCommandHandler : IRequestHandler<BecomeAuthorCommand, Result>
{
    private readonly ApplicationSettings settings;
    private readonly UserManager<User> userManager;
    private readonly IMessagePublisher publisher;
    private readonly ICurrentUser currentUser;

    public BecomeAuthorCommandHandler(
        IMessagePublisher publisher,
        ICurrentUser currentUser,
        UserManager<User> userManager,        
        IOptions<ApplicationSettings> appSettings)
    {
        this.settings = appSettings.Value;
        this.userManager = userManager;
        this.publisher = publisher;
        this.currentUser = currentUser;
    }

    public async Task<Result> Handle(BecomeAuthorCommand request, CancellationToken cancellationToken) 
    {
        var user = await this.userManager.FindByIdAsync(this.currentUser.UserId);
        Guard.ForNull<User, UserNotFoundException>(user, message: Messages.UserNotFoundError);

        if (await this.userManager.IsInRoleAsync(user, this.settings.Roles.Author))
        {
            return Result.Fail(Messages.AlreadyAuthorError);
        }

        var identityResult = await this.userManager.AddToRoleAsync(user, this.settings.Roles.Author);
        var errors = identityResult.Errors.Select(e => new ResultError(e.Code, e.Description));

        if (identityResult.Succeeded)
        {
            var newAuthorMsg = new BecomeAuthorMessage(user.Id, user.Email, user.FirstName, request.Alias);
            await this.publisher.Publish(newAuthorMsg, cancellationToken);
        }

        return identityResult.Succeeded ?
            Result.Success(Messages.BecomeAuthorSuccess) :
            Result.Fail(Messages.BecomeAuthorError, errors);
    }
}