namespace StoryBooks.Features.Identity.Infrastructure.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using StoryBooks.Features.Application;
using StoryBooks.Features.Application.Commands;
using StoryBooks.Features.Identity.Application.Commands.ChangePassword;
using StoryBooks.Features.Identity.Application.Commands.LoginUser;
using StoryBooks.Features.Identity.Application.Commands.RegisterUser;
using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Domain.Factories;

using System.Linq;
using System.Threading.Tasks;

using static StoryBooks.Features.Identity.Infrastructure.IdentityInfrastructureConstants;

public class IdentityService : IIdentityService
{
    private readonly ApplicationSettings settings;
    private readonly UserManager<User> userManager;
    private readonly IUserFactory userFactory;
    private readonly IIdentityEmailService emailService;
    private readonly ITokenGeneratorService tokenGenerator;

    public IdentityService(
        IOptions<ApplicationSettings> settings,
        UserManager<User> userManager,
        IUserFactory userFactory,
        IIdentityEmailService emailService,
        ITokenGeneratorService tokenGenerator)
    {
        this.settings = settings.Value;
        this.userManager = userManager;
        this.userFactory = userFactory;
        this.emailService = emailService;
        this.tokenGenerator = tokenGenerator;
    }

    public async Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput)
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

    public async Task<Result<LoginUserSuccessModel>> Login(LoginUserInputModel userInput)
    {
        var user = await this.userManager.FindByNameAsync(userInput.Email);
        if (user is null)
        {
            return Result<LoginUserSuccessModel>.Fail(Messages.InvalidLoginError);
        }

        var passwordValid = await this.userManager.CheckPasswordAsync(user, userInput.Password);
        if (!passwordValid)
        {
            return Result<LoginUserSuccessModel>.Fail(Messages.InvalidLoginError);
        }

        var roles = await this.userManager.GetRolesAsync(user);
        var token = this.tokenGenerator.GenerateToken(user, roles);

        return Result<LoginUserSuccessModel>.Success(
            Messages.LoggedSuccessfully,
            new LoginUserSuccessModel(user.Id, token));
    }

    public async Task<Result<IdModel<string>>> Register(RegisterUserInputModel userInput)
    {
        var user = this.userFactory
            .AddEmail(userInput.Email)
            .AddFirstName(userInput.FirstName)
            .AddLastName(userInput.LastName)
            .Build();

        bool userCreated = false;
        var identityResult = await this.userManager.CreateAsync(user, userInput.Password);
        if (identityResult.Succeeded)
        {
            userCreated = true;
            identityResult = await this.userManager.AddToRoleAsync(user, settings.Roles.User);
        }

        if (identityResult.Succeeded)
        {
            await this.userManager.SetEmailAsync(user, userInput.Email);
        }

        if (identityResult.Succeeded)
        {
            await this.emailService.SendUserRegisteredEmail(user);
        }
        else if (userCreated)
        {
            await this.userManager.DeleteAsync(user);
        }

        var errors = identityResult.Errors.Select(e => new ResultError(e.Code, e.Description));

        return identityResult.Succeeded ?
            Result<IdModel<string>>.Success(
                Messages.UserRegistrationSuccess,
                new IdModel<string>(user.Id)) :
            Result<IdModel<string>>.Fail(Messages.UserRegistrationError, errors);
    }
}