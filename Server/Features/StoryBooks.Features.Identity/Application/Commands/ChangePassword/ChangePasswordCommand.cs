namespace StoryBooks.Features.Identity.Application.Commands.ChangePassword;

using MediatR;

using Microsoft.AspNetCore.Identity;

using StoryBooks.Features.Application;
using StoryBooks.Features.Identity.Domain.Entities;

using System.Threading;
using System.Threading.Tasks;

using static StoryBooks.Features.Identity.Application.IdentityApplicationConstants;

public class ChangePasswordCommand : ChangePasswordInputModel, IRequest<Result>
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly UserManager<User> userManager;

        public ChangePasswordCommandHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Result> Handle(ChangePasswordCommand changePasswordInput, CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByIdAsync(changePasswordInput.Id);

            if (user is null)
            {
                return Result.Fail(Messages.UserNotFoundError);
            }

            var identityResult = await this.userManager.ChangePasswordAsync(
                user,
                changePasswordInput.Password,
                changePasswordInput.NewPassword);

            var errors = identityResult.Errors.Select(e => new ResultError(e.Code, e.Description));

            return identityResult.Succeeded ?
                Result.Success(Messages.PasswordChanged) :
                Result.Fail(Messages.PasswordChangeError, errors);
        }
    }
}