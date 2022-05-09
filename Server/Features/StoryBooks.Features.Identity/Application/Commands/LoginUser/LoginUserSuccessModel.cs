namespace StoryBooks.Features.Identity.Application.Commands.LoginUser;

using StoryBooks.Features.Identity.Application.Services;

public record LoginUserSuccessModel(
    string UserId, 
    string Token, 
    DateTime? Expires) : TokenModel(Token, Expires);