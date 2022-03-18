namespace StoryBooks.Features.Identity.Application.Commands.RegisterUser;

using StoryBooks.Features.Identity.Application.Commands.LoginUser;

public class RegisterUserInputModel : LoginUserInputModel
{
    public string ConfirmPassword { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;
}