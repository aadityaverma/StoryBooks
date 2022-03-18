namespace StoryBooks.Features.Identity.Application.Commands.LoginUser;

public class LoginUserSuccessModel
{
    public LoginUserSuccessModel(string userId, string token)
    {
        this.UserId = userId;
        this.Token = token;
    }

    public string UserId { get; }

    public string Token { get; }
}