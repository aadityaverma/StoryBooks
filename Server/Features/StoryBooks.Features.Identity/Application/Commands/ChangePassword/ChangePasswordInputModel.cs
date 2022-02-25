namespace StoryBooks.Features.Identity.Application.Commands.ChangePassword
{
    public class ChangePasswordInputModel
    {
        public string Id { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string NewPassword { get; set; } = default!;
    }
}
