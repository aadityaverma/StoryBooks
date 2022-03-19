namespace StoryBooks.Features.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;

public abstract class EndpointRegister : IEndpointRegister
{
    public abstract void AddEndpoints(WebApplication app, string? endpointPrefix = null);

    protected string GetTag<T>()
        where T : EndpointRegister
    {
        var name = typeof(T).Name;
        return name.Replace(nameof(EndpointRegister), string.Empty);
    }
}