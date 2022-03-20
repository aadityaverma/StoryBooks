namespace StoryBooks.Features.Identity.Presentation.Endpoints;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using StoryBooks.Features.Application;
using StoryBooks.Features.Application.Commands;
using StoryBooks.Features.Identity.Application.Commands.LoginUser;
using StoryBooks.Features.Identity.Application.Commands.RegisterUser;
using StoryBooks.Features.Identity.Application.Commands.UpdateDetails;
using StoryBooks.Features.Identity.Application.Queries.PersonalAccount;
using StoryBooks.Features.Presentation.Endpoints;
using StoryBooks.Features.Presentation.Extensions;

public class AccountEndpointRegister : EndpointRegister
{
    public override void AddEndpoints(WebApplication app, string? endpointPrefix = null)
    {
        string prefix = endpointPrefix ?? string.Empty;
        string endpoint = $"{prefix}/account";
        string tag = GetTag<AccountEndpointRegister>();

        app.MapGet(endpoint, Details)
           .Produces(StatusCodes.Status200OK, typeof(PersonalDetailsModel))
           .Produces(StatusCodes.Status404NotFound)
           .WithName($"{tag}{nameof(Details)}")
           .WithTags(tag)
           .RequireAuthorization();

        app.MapPost(endpoint, Register)
           .Produces(StatusCodes.Status201Created, typeof(IdModel<string>))
           .Produces(StatusCodes.Status400BadRequest, typeof(IEnumerable<ResultError>))
           .WithName($"{tag}{nameof(Register)}")
           .WithTags(tag);

        app.MapPut(endpoint, Update)
           .Produces(StatusCodes.Status200OK, typeof(string))
           .Produces(StatusCodes.Status400BadRequest, typeof(IEnumerable<ResultError>))
           .Produces(StatusCodes.Status404NotFound)
           .WithName($"{tag}{nameof(Update)}")
           .WithTags(tag)
           .RequireAuthorization();

        app.MapPost($"{endpoint}/login", Login)
           .Produces(StatusCodes.Status200OK, typeof(LoginUserSuccessModel))
           .Produces(StatusCodes.Status400BadRequest, typeof(IEnumerable<ResultError>))
           .Produces(StatusCodes.Status404NotFound)
           .WithName($"{tag}{nameof(Login)}")
           .WithTags(tag);
    }

    internal static async Task<IResult> Details(
        IMediator mediator, 
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetPersonalDetailsQuery(), cancellationToken).ToIResult();
    }

    internal static async Task<IResult> Register(
        IMediator mediator, 
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(command, cancellationToken).ToIResult();
    }

    internal static async Task<IResult> Update(
        IMediator mediator, 
        UpdateUserDetailsCommand command,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(command, cancellationToken).ToIResult();
    }

    internal static async Task<IResult> Login(
        IMediator mediator, 
        LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(command, cancellationToken).ToIResult();
    }
}