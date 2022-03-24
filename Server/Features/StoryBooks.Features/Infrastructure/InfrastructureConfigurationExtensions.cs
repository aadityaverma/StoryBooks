namespace StoryBooks.Features.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StoryBooks.Features.Domain.Interfaces;
using StoryBooks.Features.Infrastructure.Persistence;

using System.Reflection;

internal static class InfrastructureConfigurationExtensions
{
    private const string DefaultConnectionName = "DefaultConnection";

    internal static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        Assembly featureAssembly)
        => services.AddRepositories(featureAssembly);

    internal static IServiceCollection AddInfrastructureLayer<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly featureAssembly,
        string? connectionString)
        where TContext : DbContext
        => services.AddDatabase<TContext>(configuration, connectionString)
                   .AddRepositories(featureAssembly)
                   .AddDbInitializers(featureAssembly);

    private static IServiceCollection AddDatabase<TContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string? connectionString)
        where TContext : DbContext
    {
        var connection = configuration.GetConnectionString(connectionString);
        return services
            .AddDbContext<TContext>(options => options
                .UseSqlServer(connection ?? GetDefaultConnectionString(configuration),
                    sqlServer => sqlServer
                        .MigrationsAssembly(typeof(TContext)
                            .Assembly.FullName)));
    }

    private static string GetDefaultConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString(DefaultConnectionName);

    private static IServiceCollection AddRepositories(
        this IServiceCollection services,
        Assembly featureAssembly)
        => services
            .Scan(scan => scan
                .FromAssemblies(featureAssembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IDomainRepository<>)))
                .AsMatchingInterface()
                .WithTransientLifetime());

    private static IServiceCollection AddDbInitializers(
        this IServiceCollection services,
        Assembly featureAssembly)
    {
        return services.Scan(scan => scan
            .FromAssemblies(featureAssembly)
            .AddClasses(classes => classes
                .AssignableTo(typeof(IDataInitializer)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());
    }
}