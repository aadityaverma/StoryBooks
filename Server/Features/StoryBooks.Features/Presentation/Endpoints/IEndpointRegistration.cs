namespace StoryBooks.Features.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;

public interface IEndpointRegister
{
    void AddEndpoints(WebApplication app, string? endpointPrefix = null);
}
