namespace StoryBooks.Features.Authors.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using StoryBooks.Features.Presentation.Endpoints;

public class AuthorEndpointRegister : EndpointRegister
{
    public override void AddEndpoints(WebApplication app, string? endpointPrefix)
    {
        string prefix = endpointPrefix ?? string.Empty;
        string tag = this.GetTag<AuthorEndpointRegister>();

        app.MapGet($"{prefix}/list", this.GetList)
           .Produces<IEnumerable<int>>(StatusCodes.Status200OK)
           .WithName($"{tag}{nameof(GetList)}")
           .WithTags(tag)
           .RequireAuthorization();
    }

    internal IResult GetList(int n)
    {
        var list = new List<int>(n);
        for (int i = 0; i < n; i++)
        {
            list.Add(i);
        }

        return Results.Ok(list);
    }
}