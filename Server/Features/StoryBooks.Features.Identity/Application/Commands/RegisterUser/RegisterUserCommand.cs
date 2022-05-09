namespace StoryBooks.Features.Identity.Application.Commands.RegisterUser;

using MediatR;

using StoryBooks.Features.Application;
using StoryBooks.Features.Application.Commands;
using StoryBooks.Features.Identity.Application.Commands.LoginUser;

public record RegisterUserCommand(
    string Email, 
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName) : LoginUserCommand(Email, Password), IRequest<Result<IdModel<string>>>;