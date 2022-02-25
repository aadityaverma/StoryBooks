namespace StoryBooks.Features.Common.Domain
{
    using Microsoft.Extensions.DependencyInjection;

    using StoryBooks.Features.Common.Domain.Interfaces;

    using System.Reflection;

    internal static class DomainConfigurationExtensions
    {
        internal static IServiceCollection AddDomainLayer(
            this IServiceCollection services, Assembly domainAssembly)
                => services
                    .Scan(scan => scan
                        .FromAssemblies(domainAssembly)
                        .AddClasses(classes => classes
                            .AssignableTo(typeof(IFactory<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime())
                    .Scan(scan => scan
                        .FromAssemblies(domainAssembly)
                        .AddClasses(classes => classes
                            .AssignableTo(typeof(IFactoryWithBuilder<>)))
                        .AsMatchingInterface()
                        .WithTransientLifetime());
    }
}
