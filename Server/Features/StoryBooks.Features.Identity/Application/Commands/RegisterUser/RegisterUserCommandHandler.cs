namespace StoryBooks.Features.Identity.Application.Commands.RegisterUser;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using StoryBooks.Features.Application.Commands;
using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Domain.Factories;

using System.Web;

using static StoryBooks.Features.Identity.Application.IdentityApplicationConstants;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<IdModel<string>>>
{
    private readonly ApplicationSettings appSettings;
    private readonly IdentitySettings identitySettings;
    private readonly UserManager<User> userManager;
    private readonly IUserFactory userFactory;
    private readonly IIdentityEmailService emailService;

    public RegisterUserCommandHandler(
        IOptions<ApplicationSettings> appSettings,
        IOptions<IdentitySettings> identitySettings,
        UserManager<User> userManager,
        IUserFactory userFactory,
        IIdentityEmailService emailService)
    {
        this.appSettings = appSettings.Value;
        this.identitySettings = identitySettings.Value;
        this.userManager = userManager;
        this.userFactory = userFactory;
        this.emailService = emailService;
    }

    public async Task<Result<IdModel<string>>> Handle(RegisterUserCommand userInput, CancellationToken cancellationToken)
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
            identityResult = await this.userManager.AddToRoleAsync(user, this.appSettings.Roles.User);
        }

        if (identityResult.Succeeded)
        {
            await this.userManager.SetEmailAsync(user, userInput.Email);
        }

        if (identityResult.Succeeded && userInput.Email == this.identitySettings.MainAdminEmail)
        {
            identityResult = await this.userManager.AddToRoleAsync(user, this.appSettings.Roles.Admin);
            identityResult = await this.userManager.AddToRoleAsync(user, this.appSettings.Roles.Moderator);
        }

        if (identityResult.Succeeded)
        {
            string? token = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            string? urlToken = HttpUtility.UrlEncode(token);
            await this.emailService.SendUserRegisteredEmail(user, urlToken);
        }

        if (!identityResult.Succeeded && userCreated)
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