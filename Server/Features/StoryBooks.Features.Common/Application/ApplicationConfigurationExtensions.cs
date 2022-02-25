namespace StoryBooks.Features.Common.Application
{
    using FluentValidation.AspNetCore;

    using MediatR;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using StoryBooks.Features.Common.Application.Behaviours;
    using StoryBooks.Features.Common.Application.Mapping;

    using System.Reflection;

    internal static class ApplicationConfigurationExtensions
    {
        internal static IServiceCollection AddApplicationLayer(
            this IServiceCollection services,
            IConfiguration configuration,
            Assembly featureAssembly)
            => services
                .AddSettingsSection<ApplicationSettings>(configuration)
                .AddAutoMapperProfile(featureAssembly)
                .AddFluentValidation(validation => validation
                    .RegisterValidatorsFromAssembly(featureAssembly))
                .AddMediatR(featureAssembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        private static IServiceCollection AddAutoMapperProfile(
            this IServiceCollection services,
            Assembly assembly)
            => services
                .AddAutoMapper(
                    (_, config) => config
                        .AddProfile(new MappingProfile(assembly)),
                    Array.Empty<Assembly>());

    }
}
