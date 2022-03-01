namespace StoryBooks.Features.Identity.Application.Queries.PersonalAccount
{
    using AutoMapper;

    using StoryBooks.Features.Common.Application.Mapping;
    using StoryBooks.Features.Identity.Domain.Entities;

    public class PersonalDetailsModel : IMapFrom<User>
    {
        public string Id { get; internal set; } = default!;

        public string Email { get; internal set; } = default!;

        public string FirstName { get; internal set; } = default!;

        public string LastName { get; internal set; } = default!;

        public string? PhoneNumber { get; internal set; }

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<User, PersonalDetailsModel>()
                     .ForMember(u => u.Email, cfg => cfg.MapFrom(u => u.UserName));
    }
}
