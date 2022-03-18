namespace StoryBooks.Features.Application.Commands;

using MediatR;

using StoryBooks.Features.Application.Interfaces;

using System.Threading;
using System.Threading.Tasks;

public abstract class UserCommandHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    protected readonly ICurrentUser currentUser;

    public UserCommandHandler(ICurrentUser currentUser)
    {
        this.currentUser = currentUser;
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}