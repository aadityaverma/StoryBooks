namespace StoryBooks.Features.Identity.Application.Commands.LoginUser;

using MediatR;

using StoryBooks.Features.Application;
using StoryBooks.Features.Identity.Application.Services;

using System.Threading;
using System.Threading.Tasks;

public class LoginUserCommand : LoginUserInputModel, IRequest<Result<LoginUserSuccessModel>>
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginUserSuccessModel>>
    {
        private readonly IIdentityService identityService;

        public LoginUserCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<Result<LoginUserSuccessModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            => await this.identityService.Login(request);
    }
}