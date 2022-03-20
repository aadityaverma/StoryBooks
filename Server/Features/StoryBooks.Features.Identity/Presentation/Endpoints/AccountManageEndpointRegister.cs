namespace StoryBooks.Features.Identity.Presentation.Endpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using StoryBooks.Features.Application;
using StoryBooks.Features.Identity.Application.Commands.ChangePassword;
using StoryBooks.Features.Identity.Application.Commands.ConfirmEmail;
using StoryBooks.Features.Presentation.Endpoints;
using StoryBooks.Features.Presentation.Extensions;

internal class AccountManageEndpointRegister : EndpointRegister
{
    public override void AddEndpoints(WebApplication app, string? endpointPrefix = null)
    {
        string prefix = endpointPrefix ?? string.Empty;
        string endpoint = $"{prefix}/account/manage";
        string tag = GetTag<AccountManageEndpointRegister>();

        app.MapPut($"{endpoint}/password", Password)
           .Produces(StatusCodes.Status200OK, typeof(string))
           .Produces(StatusCodes.Status400BadRequest, typeof(IEnumerable<ResultError>))
           .Produces(StatusCodes.Status404NotFound)
           .WithName($"{tag}{nameof(Password)}")
           .WithTags(tag)
           .RequireAuthorization();

        app.MapGet($"{endpoint}/email/{{userId}}/{{token}}", Email)
           .Produces(StatusCodes.Status200OK, typeof(string))
           .Produces(StatusCodes.Status400BadRequest, typeof(IEnumerable<ResultError>))
           .Produces(StatusCodes.Status404NotFound)
           .WithName($"{tag}{nameof(Email)}")
           .WithTags(tag);
    }

    internal async Task<IResult> Password(
        IMediator mediator,
        ChangePasswordCommand command,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(command, cancellationToken).ToIResult();
    }

    internal async Task<IResult> Email(
        IMediator mediator,
        string userId,
        string token,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new ConfirmEmailCommand 
        { 
            UserId = userId, 
            Token = token 
        }, cancellationToken).ToIResult();
    }
}
