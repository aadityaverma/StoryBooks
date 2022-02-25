namespace StoryBooks.Features.Identity.Application.Services
{
    using StoryBooks.Features.Common.Application;
    using StoryBooks.Features.Common.Application.Commands;
    using StoryBooks.Features.Identity.Application.Commands.ChangePassword;
    using StoryBooks.Features.Identity.Application.Commands.LoginUser;
    using StoryBooks.Features.Identity.Application.Commands.RegisterUser;

    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<Result<IdModel<string>>> Register(RegisterUserInputModel userInput);

        Task<Result<LoginUserSuccessModel>> Login(LoginUserInputModel userInput);

        Task<Result> ChangePassword(ChangePasswordInputModel changePasswordInput);
    }
}
