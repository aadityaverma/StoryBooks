namespace StoryBooks.Features.Identity.Application.Commands.RegisterUser
{
    using MediatR;

    using StoryBooks.Features.Common.Application;
    using StoryBooks.Features.Common.Application.Commands;
    using StoryBooks.Features.Identity.Application.Services;

    using System.Threading;
    using System.Threading.Tasks;

    public class RegisterUserCommand : RegisterUserInputModel, IRequest<Result<IdModel<string>>>
    {
        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<IdModel<string>>>
        {
            private readonly IIdentityService identityService;

            public RegisterUserCommandHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result<IdModel<string>>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
                => await this.identityService.Register(request);
        }
    }
}
