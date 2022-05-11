﻿namespace StoryBooks.Libraries.EventBus;

using Hangfire;
using Hangfire.SqlServer;

using MassTransit;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StoryBooks.Libraries.EventBus.Services;

using System.Data.SqlClient;
using System.Reflection;

public static class MessagesConfigurationExtensions
{
    public static IServiceCollection AddInMemoryTransit(
        this IServiceCollection services,
        params Assembly[] consumersAssemblies)
    {
        services
            .AddTransient<IMessagePublisher, MessagePublisher>()
            .AddMassTransit(mt =>
            {
                mt.SetKebabCaseEndpointNameFormatter();

                foreach (var assembly in consumersAssemblies)
                {
                    mt.AddConsumers(assembly);
                }

                mt.UsingInMemory((ctx, cfg) =>
                {
                    cfg.ConfigureEndpoints(ctx);
                });
            });

        services
            .AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                options.WaitUntilStarted = true;
                options.StartTimeout = TimeSpan.FromSeconds(10);
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });

        return services;
    }

    public static IServiceCollection AddMessagesFeature(
        this IServiceCollection services,
        IConfiguration configuration,
        params Type[] consumers)
            => services
                //TODO Add MQ Database context
                .AddMessaging(configuration, true, consumers);

    private static MessageQueueSettings GetMessageQueueSettings(IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(MessageQueueSettings));

        return new MessageQueueSettings(
            settings.GetValue<string>(nameof(MessageQueueSettings.Host)),
            settings.GetValue<string>(nameof(MessageQueueSettings.UserName)),
            settings.GetValue<string>(nameof(MessageQueueSettings.Password)));
    }

    private static string GetCronJobsConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString("CronJobsConnection");

    private static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration configuration,
        bool usePolling = true,
        params Type[] consumers)
    {
        services.AddTransient<IMessagePublisher, MessagePublisher>();

        var messageQueueSettings = GetMessageQueueSettings(configuration);

        services
            .AddMassTransit(mt =>
            {
                foreach (var consumer in consumers)
                {
                    mt.AddConsumer(consumer);
                }

                mt.AddBus(context => Bus.Factory.CreateUsingRabbitMq(rmq =>
                {
                    rmq.Host(messageQueueSettings.Host, host =>
                    {
                        host.Username(messageQueueSettings.UserName);
                        host.Password(messageQueueSettings.Password);
                    });

                    foreach (var consumer in consumers)
                    {
                        rmq.ReceiveEndpoint(consumer.FullName ?? string.Empty, endpoint =>
                        {
                            endpoint.PrefetchCount = 6;
                            endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                            endpoint.ConfigureConsumer(context, consumer);
                        });
                    }
                }));
            });

        if (usePolling)
        {
            CreateHangfireDatabase(configuration);

            services
                .AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(
                        configuration.GetCronJobsConnectionString(),
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            DisableGlobalLocks = true
                        }));

            services.AddHangfireServer();
            services.AddHostedService<MessagesHostedService>();
        }

        return services;
    }

    private static void CreateHangfireDatabase(IConfiguration configuration)
    {
        string? connectionString = configuration.GetCronJobsConnectionString();

        string? dbName = connectionString
            .Split(";")[1]
            .Split("=")[1];

        using var connection = new SqlConnection(connectionString.Replace(dbName, "master"));

        connection.Open();

        using var command = new SqlCommand(
            $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}') create database [{dbName}];",
            connection);

        command.ExecuteNonQuery();
    }
}
