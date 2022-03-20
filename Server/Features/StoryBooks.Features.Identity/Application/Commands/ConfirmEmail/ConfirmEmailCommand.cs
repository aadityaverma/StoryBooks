namespace StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;

using MediatR;

using StoryBooks.Features.Application;
using StoryBooks.Features.Identity.Application.Services;

using System.Threading;
using System.Threading.Tasks;

public class ConfirmEmailCommand : ConfirmEmailInputModel, IRequest<Result>
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
    {
        private readonly IIdentityService identityService;

        public ConfirmEmailCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
            => identityService.ConfirmEmail(request);
    }
}