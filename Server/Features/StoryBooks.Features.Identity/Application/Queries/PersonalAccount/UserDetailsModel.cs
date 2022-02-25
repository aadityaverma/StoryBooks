namespace StoryBooks.Features.Identity.Application.Queries.PersonalAccount
{
    public class UserDetailsModel
    {
        public string Id { get; internal set; } = default!;

        public string Email { get; internal set; } = default!;

        public string FirstName { get; internal set; } = default!;

        public string? PhoneNumber { get; internal set; }
    }
}
