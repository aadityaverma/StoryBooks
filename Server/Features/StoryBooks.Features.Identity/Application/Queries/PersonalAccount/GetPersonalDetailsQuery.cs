namespace StoryBooks.Features.Identity.Application.Queries.PersonalAccount;

using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;

using StoryBooks.Features.Application.Interfaces;
using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Domain.Exceptions;
using StoryBooks.Libraries.Validation;

using System.Threading;
using System.Threading.Tasks;

public class GetPersonalDetailsQuery : IRequest<PersonalDetailsModel>
{
    public class GetPersonalDetailsQueryHandler : IRequestHandler<GetPersonalDetailsQuery, PersonalDetailsModel>
    {
        private readonly ICurrentUser currentUser;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public GetPersonalDetailsQueryHandler(
            IMapper mapper,
            ICurrentUser currentUser,
            UserManager<User> userManager)
        {
            this.currentUser = currentUser;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<PersonalDetailsModel> Handle(GetPersonalDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByNameAsync(this.currentUser.Email);
            Guard.ForNull<User, UserNotFoundException>(user);

            var userResponse = this.mapper.Map<PersonalDetailsModel>(user) ?? default!;
            userResponse.Roles = await this.userManager.GetRolesAsync(user);
            return userResponse;
        }
    }
}