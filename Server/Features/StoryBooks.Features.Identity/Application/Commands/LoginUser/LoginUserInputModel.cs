namespace StoryBooks.Features.Identity.Application.Commands.LoginUser;

public class LoginUserInputModel
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}