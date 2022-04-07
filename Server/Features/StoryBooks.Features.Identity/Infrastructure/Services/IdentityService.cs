﻿namespace StoryBooks.Features.Identity.Infrastructure.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

using StoryBooks.Features.Application;
using StoryBooks.Features.Application.Commands;
using StoryBooks.Features.Identity.Application.Commands.ChangePassword;
using StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;
using StoryBooks.Features.Identity.Application.Commands.LoginUser;
using StoryBooks.Features.Identity.Application.Commands.RegisterUser;
using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Domain.Factories;

using System.Linq;
using System.Threading.Tasks;
using System.Web;

using static StoryBooks.Features.Identity.Infrastructure.IdentityInfrastructureConstants;

public class IdentityService : IIdentityService
{
    private readonly ApplicationSettings appSettings;
    private readonly IdentitySettings identitySettings;
    private readonly UserManager<User> userManager;
    private readonly IUserFactory userFactory;
    private readonly IIdentityEmailService emailService;
    private readonly IAuthTokenGeneratorService tokenGenerator;

    public IdentityService(
        IOptions<ApplicationSettings> appSettings,
        IOptions<IdentitySettings> identitySettings,
        UserManager<User> userManager,
        IUserFactory userFactory,
        IIdentityEmailService emailService,
        IAuthTokenGeneratorService tokenGenerator)
    {
        this.appSettings = appSettings.Value;
        this.identitySettings = identitySettings.Value;
        this.userManager = userManager;
        this.userFactory = userFactory;
        this.emailService = emailService;
        this.tokenGenerator = tokenGenerator;
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

    public async Task<Result<LoginUserSuccessModel>> Login(LoginUserInputModel userInput)
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

    public async Task<Result> ConfirmEmail(ConfirmEmailCommand request)
    {
        var user = await this.userManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            return Result.NotFound(Messages.InvalidLoginError);
        }

        string? token = HttpUtility.UrlDecode(request.Token);
        var identityResult = await this.userManager.ConfirmEmailAsync(user, token);
        var errors = identityResult.Errors.Select(e => new ResultError(e.Code, e.Description));

        return identityResult.Succeeded ?
            Result.Success(Messages.EmailConfirmed) :
            Result.Fail(Messages.EmailConfirmError, errors);
    }
}