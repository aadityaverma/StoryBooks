namespace StoryBooks.Features.Identity.Infrastructure.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    using StoryBooks.Features.Common.Application;
    using StoryBooks.Features.Common.Application.Commands;
    using StoryBooks.Features.Identity.Application.Commands.ChangePassword;
    using StoryBooks.Features.Identity.Application.Commands.LoginUser;
    using StoryBooks.Features.Identity.Application.Commands.RegisterUser;
    using StoryBooks.Features.Identity.Application.Services;
    using StoryBooks.Features.Identity.Domain.Entities;
    using StoryBooks.Features.Identity.Domain.Factories;
    using StoryBooks.Libraries.Email.Services;

    using System.Linq;
    using System.Threading.Tasks;

    using static StoryBooks.Features.Identity.Infrastructure.InfrastructureConstants;


    public class IdentityService : IIdentityService
    {
        private readonly ApplicationSettings settings;
        private readonly ITokenGeneratorService tokenGenerator;
        private readonly UserManager<User> userManager;
        private readonly IUserFactory userFactory;
        private readonly IEmailSender emailSender;

        public IdentityService(
            IOptions<ApplicationSettings> settings,
            ITokenGeneratorService tokenGenerator,
            UserManager<User> userManager,
            IUserFactory userFactory,
            IEmailSender emailSender)
        {
            this.settings = settings.Value;
            this.tokenGenerator = tokenGenerator;
            this.userManager = userManager;
            this.userFactory = userFactory;
            this.emailSender = emailSender;
        }

        public async Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput)
        {
            var user = await this.userManager.FindByIdAsync(changePasswordInput.Id);

            if (user is null)
            {
                return Result.Fail(IdentityMessages.UserNotFoundError);
            }

            var identityResult = await this.userManager.ChangePasswordAsync(
                user,
                changePasswordInput.Password,
                changePasswordInput.NewPassword);

            var errors = identityResult.Errors.Select(e => new ResultError(e.Code, e.Description));

            return identityResult.Succeeded
                ? Result.Success(IdentityMessages.PasswordChanged)
                : Result.Fail(IdentityMessages.PasswordChangeError, errors);
        }

        public async Task<Result<LoginUserSuccessModel>> Login(LoginUserInputModel userInput)
        {
            var user = await this.userManager.FindByNameAsync(userInput.Email);
            if (user is null)
            {
                return Result<LoginUserSuccessModel>.Fail(IdentityMessages.InvalidLoginError);
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, userInput.Password);
            if (!passwordValid)
            {
                return Result<LoginUserSuccessModel>.Fail(IdentityMessages.InvalidLoginError);
            }

            var roles = await this.userManager.GetRolesAsync(user);
            var token = this.tokenGenerator.GenerateToken(user, roles);

            return Result<LoginUserSuccessModel>.Success(
                IdentityMessages.LoggedSuccessfully, new LoginUserSuccessModel(user.Id, token));
        }

        public async Task<Result<IdModel<string>>> Register(RegisterUserInputModel userInput)
        {
            var user = this.userFactory
                .AddEmail(userInput.Email)
                .AddFirstName(userInput.FirstName)
                .AddLastName(userInput.LastName)
                .Build();

            var identityResult = await this.userManager.CreateAsync(user, userInput.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await this.userManager.AddToRoleAsync(user, settings.Roles.User);
                await this.emailSender.SendEmailAsync(user.UserName, "Registration confirmation", "You registered into Story books!");
            }
            
            var errors = identityResult.Errors.Select(e => new ResultError(e.Code, e.Description));

            return identityResult.Succeeded
                ? Result<IdModel<string>>.Success(
                    IdentityMessages.UserRegistrationSuccess, new IdModel<string>(user.Id))
                : Result<IdModel<string>>.Fail(IdentityMessages.UserRegistrationError, errors);
        }
    }
}
