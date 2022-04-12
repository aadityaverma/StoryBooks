namespace StoryBooks.Features.Identity.Application.Commands.ResendConfirmEmail;

using MediatR;

using StoryBooks.Features.Application;

using System.Threading;
using System.Threading.Tasks;

public class ResendConfirmEmailCommand : IRequest<Result>
{
    public class ResendConfirmEmailCommandHandler : IRequestHandler<ResendConfirmEmailCommand, Result>
    {
        public Task<Result> Handle(ResendConfirmEmailCommand request, CancellationToken cancellationToken)
            => throw new NotImplementedException();
    }
}
