namespace StoryBooks.Features.Identity.Resources.EmailTemplates.EmailConfirmation;

using StoryBooks.Features.Application.Mapping;
using StoryBooks.Features.Identity.Domain.Entities;

public class EmailConfirmationEmailModel : IMapFrom<User>
{
    public string Name { get; private set; } = default!;

    public string Email { get; private set; } = default!;

    public string ConfirmUrl { get; internal set; } = default!;

    public string ServerUrl { get; internal set; } = default!;
}
