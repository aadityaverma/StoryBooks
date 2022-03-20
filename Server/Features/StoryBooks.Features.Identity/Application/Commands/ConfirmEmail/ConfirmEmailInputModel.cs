namespace StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;

public class ConfirmEmailInputModel
{
    public string UserId { get; set; } = default!;

    public string Token { get; set; } = default!;
}