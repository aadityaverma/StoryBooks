namespace StoryBooks.Features.Identity.Application.Services;

using StoryBooks.Features.Application;
using StoryBooks.Features.Application.Commands;
using StoryBooks.Features.Identity.Application.Commands.ChangePassword;
using StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;
using StoryBooks.Features.Identity.Application.Commands.LoginUser;
using StoryBooks.Features.Identity.Application.Commands.RegisterUser;

using System.Threading.Tasks;

public interface IIdentityService
{
    Task<Result<IdModel<string>>> Register(RegisterUserInputModel userInput);

    Task<Result<LoginUserSuccessModel>> Login(LoginUserInputModel userInput);

    Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput);
    Task<Result> ConfirmEmail(ConfirmEmailCommand request);
}