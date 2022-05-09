namespace StoryBooks.Features.Identity.Application.Commands.LoginUser;

using MediatR;

using StoryBooks.Features.Application;

public record LoginUserCommand(
    string Email, 
    string Password) : IRequest<Result<LoginUserSuccessModel>>;