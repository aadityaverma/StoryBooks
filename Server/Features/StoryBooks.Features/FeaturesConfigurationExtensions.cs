namespace StoryBooks.Features;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StoryBooks.Features.Application;
using StoryBooks.Features.Domain;
using StoryBooks.Features.Infrastructure;
using StoryBooks.Features.Presentation;

using System.Reflection;

public static class FeaturesConfigurationExtensions
{
    public static IServiceCollection ConfigureFeature(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly featureAssembly)
            => services
                .AddDomainLayer(featureAssembly)
                .AddApplicationLayer(configuration, featureAssembly)
                .AddInfrastructureLayer(featureAssembly)
                .AddPresentationLayer(featureAssembly);

    public static IServiceCollection ConfigureFeature<TContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly featureAssembly,
            string? connectionString)
        where TContext : DbContext
            => services
                .AddDomainLayer(featureAssembly)
                .AddApplicationLayer(configuration, featureAssembly)
                .AddInfrastructureLayer<TContext>(configuration, featureAssembly, connectionString)
                .AddPresentationLayer(featureAssembly);
}