namespace StoryBooks.Features.Common.Presentation;

using Microsoft.Extensions.DependencyInjection;

using StoryBooks.Features.Common.Application.Interfaces;
using StoryBooks.Features.Common.Presentation.Services;

internal static class PresentationConfigurationExtensions
{
    internal static IServiceCollection AddPresentationLayer(this IServiceCollection services)
        => services.AddScoped<ICurrentUser, CurrentUserService>();
}