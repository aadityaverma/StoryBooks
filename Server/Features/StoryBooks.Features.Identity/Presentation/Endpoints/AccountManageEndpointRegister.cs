﻿namespace StoryBooks.Features.Identity.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using StoryBooks.Features.Application;
using StoryBooks.Features.Identity.Application.Commands.BecomeAuthor;
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
        string tag = this.GetTag<AccountManageEndpointRegister>();

        app.MapPut($"{endpoint}/password", Password)
           .Produces(StatusCodes.Status200OK, typeof(string))
           .Produces(StatusCodes.Status400BadRequest, typeof(IEnumerable<ResultError>))
           .Produces(StatusCodes.Status404NotFound)
           .WithName($"{tag}{nameof(Password)}")
           .WithTags(tag)
           .RequireAuthorization();

        app.MapGet($"{endpoint}/email/{{userId}}/{{token}}", Email)
           .Produces(StatusCodes.Status307TemporaryRedirect)
           .WithName($"{tag}{nameof(Email)}")
           .WithTags(tag);

        app.MapPost($"{endpoint}/become-author", BecomeAuthor)
           .Produces(StatusCodes.Status200OK, typeof(string))
           .Produces(StatusCodes.Status400BadRequest, typeof(IEnumerable<ResultError>))
           .Produces(StatusCodes.Status404NotFound)
           .WithName($"{tag}{nameof(BecomeAuthor)}")
           .WithTags(tag)
           .RequireAuthorization();
    }

    internal static async Task<IResult> Password(
        IMediator mediator,
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken).ToIResult();

    internal static async Task<IResult> Email(
        IMediator mediator,
        string userId,
        string token,
        CancellationToken cancellationToken)
            => await mediator.Send(new ConfirmEmailCommand(userId, token), cancellationToken).ToIResult();

    internal static async Task<IResult> BecomeAuthor(
        IMediator mediator,
        BecomeAuthorCommand request,
        CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken).ToIResult();
}
