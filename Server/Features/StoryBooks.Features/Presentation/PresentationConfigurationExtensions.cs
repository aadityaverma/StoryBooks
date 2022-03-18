namespace StoryBooks.Features.Presentation;

using Microsoft.Extensions.DependencyInjection;

using StoryBooks.Features.Application.Interfaces;
using StoryBooks.Features.Presentation.Services;

internal static class PresentationConfigurationExtensions
{
    internal static IServiceCollection AddPresentationLayer(this IServiceCollection services)
        => services.AddScoped<ICurrentUser, CurrentUserService>();
}