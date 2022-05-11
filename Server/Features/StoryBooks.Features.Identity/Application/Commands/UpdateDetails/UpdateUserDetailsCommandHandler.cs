namespace StoryBooks.Features.Identity.Application.Commands.UpdateDetails;

using Microsoft.AspNetCore.Identity;

using StoryBooks.Features.Application.Interfaces;
using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Domain.Exceptions;
using StoryBooks.Libraries.Validation;

using static StoryBooks.Features.Identity.Application.IdentityApplicationConstants;

public class UpdateDetailsCommandHandler : IRequestHandler<UpdateUserDetailsCommand, Result>
{
    private readonly ICurrentUser currentUser;
    private readonly UserManager<User> userManager;

    public UpdateDetailsCommandHandler(
        ICurrentUser currentUser,
        UserManager<User> userManager)
    {
        this.currentUser = currentUser;
        this.userManager = userManager;
    }

    public async Task<Result> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByNameAsync(this.currentUser.Email);
        Guard.ForNull<User, UserNotFoundException>(user);

        user.SetFirstName(request.FirstName)
            .SetLastName(request.LastName)
            .SetPhoneNumber(request.PhoneNumber)
            .SetDateOfBirth(request.DateOfBirth)
            .SetGender(request.Gender);

        var identityResult = await this.userManager.UpdateAsync(user);
        var errors = identityResult.Errors.Select(e => new ResultError(e.Code, e.Description));

        return identityResult.Succeeded
            ? Result.Success(Messages.UserUpdateSuccess)
            : Result.Fail(Messages.UserUpdateError, errors);
    }
}