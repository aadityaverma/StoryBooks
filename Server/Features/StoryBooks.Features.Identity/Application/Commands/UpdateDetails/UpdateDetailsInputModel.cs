namespace StoryBooks.Features.Identity.Application.Commands.UpdateDetails
{
    public class UpdateDetailsInputModel
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? PhoneNumber { get; set; }
    }
}
