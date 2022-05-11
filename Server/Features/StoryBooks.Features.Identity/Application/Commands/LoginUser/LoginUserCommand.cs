namespace StoryBooks.Features.Identity.Application.Commands.LoginUser;

public record LoginUserCommand(
    string Email, 
    string Password) : IRequest<Result<LoginUserSuccessModel>>;