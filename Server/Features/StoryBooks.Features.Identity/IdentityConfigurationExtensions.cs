namespace StoryBooks.Features.Identity;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StoryBooks.Features;
using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Infrastructure.Persistence;
using StoryBooks.Features.Identity.Infrastructure.Services;

public static class IdentityConfigurationExtensions
{
    public static IServiceCollection AddIdentityFeature(
        this IServiceCollection services,
        IConfiguration configuration)
            => services
                .AddIdentitySettings(configuration)
                .ConfigureFeature<IdentityUserDbContext>(
                        configuration, typeof(IdentityConfigurationExtensions).Assembly, IdentitySettings.FeatureName)
                .AddIdentityLayer<IdentityUserDbContext>(configuration);

    private static IServiceCollection AddIdentityLayer<TContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TContext : IdentityUserDbContext
    {
        var settings = configuration.GetSection(nameof(IdentitySettings));

        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength =
                    settings.GetValue<int>(nameof(IdentitySettings.MinPasswordLength));
                options.Password.RequireDigit =
                    settings.GetValue<bool>(nameof(IdentitySettings.RequireDigit));
                options.Password.RequireLowercase =
                    settings.GetValue<bool>(nameof(IdentitySettings.RequireLowercase));
                options.Password.RequireNonAlphanumeric =
                    settings.GetValue<bool>(nameof(IdentitySettings.RequireNonAlphanumeric));
                options.Password.RequireUppercase =
                    settings.GetValue<bool>(nameof(IdentitySettings.RequireUppercase));
            })
            .AddEntityFrameworkStores<TContext>()
            .AddDefaultTokenProviders();

        services.AddDataProtection()
            .PersistKeysToDbContext<TContext>();

        services.AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IIdentityEmailService, IdentityEmailService>()
                .AddSingleton<IIdentityUrlProvider, IdentityUrlProvider>()
                .AddSingleton<IAuthTokenGeneratorService, JwtTokenGeneratorService>();

        return services;
    }

    private static IServiceCollection AddIdentitySettings(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .Configure<IdentitySettings>(
                configuration.GetSection(nameof(IdentitySettings)),
                config => config.BindNonPublicProperties = true);
}