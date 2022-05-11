namespace StoryBooks.Features.Identity.Application.Commands.UpdateDetails;

using StoryBooks.Features.Identity.Domain.Entities;

public record UpdateUserDetailsCommand(
    string FirstName,
    string LastName,
    string? PhoneNumber,
    Gender? Gender,
    DateOnly? DateOfBirth) : IRequest<Result>;