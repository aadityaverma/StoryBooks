namespace StoryBooks.Features.Identity.Application.Commands.LoginUser;

using Microsoft.AspNetCore.Identity;

using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;

using static StoryBooks.Features.Identity.Application.IdentityApplicationConstants;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginUserSuccessModel>>
{
    private readonly UserManager<User> userManager;
    private readonly IAuthTokenGeneratorService tokenGenerator;

    public LoginUserCommandHandler(
        UserManager<User> userManager,
        IAuthTokenGeneratorService tokenGenerator)
    {
        this.userManager = userManager;
        this.tokenGenerator = tokenGenerator;
    }

    public async Task<Result<LoginUserSuccessModel>> Handle(LoginUserCommand userInput, CancellationToken cancellationToken)
    {
        var user = await this.userManager.FindByNameAsync(userInput.Email);
        if (user is null)
        {
            return Result<LoginUserSuccessModel>.NotFound(Messages.InvalidLoginError);
        }

        bool passwordValid = await this.userManager.CheckPasswordAsync(user, userInput.Password);
        if (!passwordValid)
        {
            return Result<LoginUserSuccessModel>.Fail(Messages.InvalidLoginError);
        }

        var roles = await this.userManager.GetRolesAsync(user);
        var token = this.tokenGenerator.GenerateToken(user, roles);

        return Result<LoginUserSuccessModel>.Success(
            Messages.LoggedSuccessfully,
            new LoginUserSuccessModel(user.Id, token.Value, token.Expires));
    }
}