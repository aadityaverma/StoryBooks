namespace StoryBooks.Features.Identity.Application.Commands.LoginUser;

using StoryBooks.Features.Identity.Application.Services;

public record LoginUserSuccessModel : TokenModel
{
    public LoginUserSuccessModel(string userId, string token, DateTime? expires)
        : base(token, expires)
    {
        this.UserId = userId;
    }

    public string UserId { get; }
}