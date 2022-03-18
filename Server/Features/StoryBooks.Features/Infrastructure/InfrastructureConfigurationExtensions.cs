namespace StoryBooks.Features.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StoryBooks.Features.Domain.Interfaces;
using StoryBooks.Features.Infrastructure.Persistence;

using System.Reflection;

internal static class InfrastructureConfigurationExtensions
{
    internal static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        Assembly assembly)
        => services.AddRepositories(assembly);

    internal static IServiceCollection AddInfrastructureLayer<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
        where TContext : DbContext
        => services.AddDatabase<TContext>(configuration)
                   .AddRepositories(assembly)
                   .AddDbInitializers(assembly);

    private static string GetDefaultConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString("DefaultConnection");

    private static IServiceCollection AddDatabase<TContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TContext : DbContext
        => services
            .AddDbContext<TContext>(options => options
                .UseSqlServer(
                    configuration.GetDefaultConnectionString(),
                    sqlServer => sqlServer
                        .MigrationsAssembly(typeof(TContext)
                            .Assembly.FullName)));

    private static IServiceCollection AddRepositories(
        this IServiceCollection services,
        Assembly assembly)
        => services
            .Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IDomainRepository<>)))
                .AsMatchingInterface()
                .WithTransientLifetime());

    private static IServiceCollection AddDbInitializers(
        this IServiceCollection services,
        Assembly assembly)
    {
        var initializers = assembly
            .GetTypes()
            .Where(t => typeof(IDataInitializer).IsAssignableFrom(t))
            .ToArray();

        return services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IDataInitializer)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}