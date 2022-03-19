namespace StoryBooks.Features.Presentation;

using Microsoft.Extensions.DependencyInjection;

using StoryBooks.Features.Application.Interfaces;
using StoryBooks.Features.Presentation.Endpoints;
using StoryBooks.Features.Presentation.Services;

using System.Reflection;

internal static class PresentationConfigurationExtensions
{
    internal static IServiceCollection AddPresentationLayer(
        this IServiceCollection services,
        Assembly featureAssemply)
        => services.AddScoped<ICurrentUser, CurrentUserService>()
                   .AddEndpointsRegisters(featureAssemply);

    private static IServiceCollection AddEndpointsRegisters(
        this IServiceCollection services,
        Assembly featureAssemply)
        => services.Scan(scan => scan
                    .FromAssemblies(featureAssemply)
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IEndpointRegister)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());
}