namespace StoryBooks.Features.Identity.Application.Commands.DeleteUser;

using MediatR;

using Microsoft.AspNetCore.Identity;

using StoryBooks.Features.Application;
using StoryBooks.Features.Application.Interfaces;
using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Domain.Exceptions;
using StoryBooks.Libraries.Validation;

using System.Threading;
using System.Threading.Tasks;

using static StoryBooks.Features.Identity.Application.IdentityApplicationConstants;

public class DeleteUserCommand : DeleteUserInputModel, IRequest<RedirectResult>
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, RedirectResult>
    {
        private readonly UserManager<User> userManager;
        private readonly IIdentityUrlProvider urlProvider;
        private readonly ICurrentUser currentUser;

        public DeleteUserCommandHandler(
            UserManager<User> userManager,
            IIdentityUrlProvider urlProvider,
            ICurrentUser currentUser)
        {
            this.userManager = userManager;
            this.urlProvider = urlProvider;
            this.currentUser = currentUser;
        }

        public async Task<RedirectResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken) 
        {
            var user = await this.userManager.FindByNameAsync(this.currentUser.Email);
            Guard.ForNull<User, UserNotFoundException>(user);

            bool passwordValid = await this.userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                return new(this.urlProvider.ClientErrorUrl(Messages.UserDeletedPasswordError));
            }

            var identityResult = await this.userManager.DeleteAsync(user);
            var errors = identityResult.Errors.Select(e => new ResultError(e.Code, e.Description));

            return identityResult.Succeeded
                ? new(this.urlProvider.ClientUrl)
                : new(this.urlProvider.ClientErrorUrl(Messages.UserDeletedError));
        }
    }
}
