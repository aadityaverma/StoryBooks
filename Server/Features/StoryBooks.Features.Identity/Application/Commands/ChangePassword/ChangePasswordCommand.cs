namespace StoryBooks.Features.Identity.Application.Commands.ChangePassword
{
    using MediatR;

    using StoryBooks.Features.Common.Application;
    using StoryBooks.Features.Identity.Application.Services;

    using System.Threading;
    using System.Threading.Tasks;

    public class ChangePasswordCommand : ChangePasswordInputModel, IRequest<Result>
    {
        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
        {
            private readonly IIdentityService identityService;

            public ChangePasswordCommandHandler(IIdentityService identityService)
            {
                this.identityService = identityService;
            }

            public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
                => await this.identityService.ChangePassword(request);
        }
    }
}