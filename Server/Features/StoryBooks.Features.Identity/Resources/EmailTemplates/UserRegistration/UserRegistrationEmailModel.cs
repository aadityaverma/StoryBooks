namespace StoryBooks.Features.Identity.Resources.EmailTemplates.UserRegistration;

using AutoMapper;

using StoryBooks.Features.Application.Mapping;
using StoryBooks.Features.Identity.Domain.Entities;

public class UserRegistrationEmailModel : IMapFrom<User>
{
    public string Id { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string Email { get; private set; } = default!;

    public string ConfirmUrl { get; internal set; } = default!;

    public string ServerUrl { get; internal set; } = default!;

    public virtual void Mapping(Profile mapper)
    {
        mapper.CreateMap<User, UserRegistrationEmailModel>()
                 .ForMember(u => u.Email, cfg => cfg.MapFrom(u => u.UserName));

        mapper.CreateMap<User, UserRegistrationEmailModel>()
            .ForMember(u => u.Name, cfg => cfg.MapFrom(u => u.FirstName + " " + u.LastName));
    }
}