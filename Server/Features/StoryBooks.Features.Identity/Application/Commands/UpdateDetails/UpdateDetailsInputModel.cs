namespace StoryBooks.Features.Identity.Application.Commands.UpdateDetails;

using StoryBooks.Features.Identity.Domain.Entities;

public class UpdateDetailsInputModel
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string? PhoneNumber { get; set; }

    public Gender? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }
}

