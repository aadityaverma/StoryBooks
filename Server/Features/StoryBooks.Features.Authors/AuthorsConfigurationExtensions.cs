namespace StoryBooks.Features.Authors;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StoryBooks.Features.Authors.Infrastructure.Persistence;
using StoryBooks.Features;
using StoryBooks.Features.Infrastructure.Exceptions;

public static class AuthorsConfigurationExtensions
{
    public static IServiceCollection AddAuthorsFeature(
        this IServiceCollection services,
        IConfiguration configuration)
            => services
                .AddAuthorsSettings(configuration)
                .ConfigureFeature<AuthorsDbContext>(
                    configuration, typeof(AuthorsConfigurationExtensions).Assembly)
                .AddAuthorsLayer();

    private static IServiceCollection AddAuthorsSettings(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .Configure<AuthorsSetttings>(
                configuration.GetSection(nameof(AuthorsSetttings)),
                config => config.BindNonPublicProperties = true);

    public static IServiceCollection AddAuthorsLayer(
        this IServiceCollection services)
            => services.AddScoped<IAuthorDbContext>(
                p => p.GetService<AuthorsDbContext>() ??
                    throw new ConfigurationException(nameof(AuthorsDbContext)));
}