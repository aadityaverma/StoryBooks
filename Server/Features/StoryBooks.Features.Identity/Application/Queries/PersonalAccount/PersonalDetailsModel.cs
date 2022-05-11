namespace StoryBooks.Features.Identity.Application.Queries.PersonalAccount;

using StoryBooks.Features.Application.Mapping;
using StoryBooks.Features.Identity.Domain.Entities;

public record PersonalDetailsModel : IMapFrom<User>
{
    public string Id { get; internal set; } = default!;

    public string Email { get; internal set; } = default!;

    public string FirstName { get; internal set; } = default!;

    public string LastName { get; internal set; } = default!;

    public string? PhoneNumber { get; internal set; }

    public bool EmailConfirmed { get; internal set; }

    public IEnumerable<string> Roles { get; internal set; } = default!;

    public virtual void Mapping(Profile mapper)
        => mapper.CreateMap<User, PersonalDetailsModel>()
                 .ForMember(m => m.Email, cfg => cfg.MapFrom(u => u.UserName));
}