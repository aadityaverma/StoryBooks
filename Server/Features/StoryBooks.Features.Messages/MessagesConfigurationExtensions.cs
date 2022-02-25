namespace StoryBooks.Features.Messages
{
    using GreenPipes;

    using Hangfire;
    using Hangfire.SqlServer;

    using MassTransit;

    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using StoryBooks.Features.Common;
    using StoryBooks.Features.Messages.Persistence;
    using StoryBooks.Features.Messages.Services;

    using System.Linq;

    public static class MessagesConfigurationExtensions
    {
        public static IServiceCollection AddMessagesFeature(
            this IServiceCollection services,
            IConfiguration configuration,
            params Type[] consumers)
                => services
                    .ConfigureFeature<MessageDbContext>(configuration, typeof(MessagesConfigurationExtensions).Assembly)
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
            services
                .AddTransient<IPublisher, Publisher>()
                .AddTransient<IMessageService, MessageService>();

            var messageQueueSettings = GetMessageQueueSettings(configuration);

            services
                .AddMassTransit(mt =>
                {
                    consumers.ForEach(consumer => mt.AddConsumer(consumer));

                    mt.AddBus(context => Bus.Factory.CreateUsingRabbitMq(rmq =>
                    {
                        rmq.Host(messageQueueSettings.Host, host =>
                        {
                            host.Username(messageQueueSettings.UserName);
                            host.Password(messageQueueSettings.Password);
                        });

                        consumers.ForEach(consumer => rmq.ReceiveEndpoint(consumer.FullName, endpoint =>
                        {
                            endpoint.PrefetchCount = 6;
                            endpoint.UseMessageRetry(retry => retry.Interval(5, 200));

                            endpoint.ConfigureConsumer(context, consumer);
                        }));
                    }));
                })
                .AddMassTransitHostedService();

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
            var connectionString = configuration.GetCronJobsConnectionString();

            var dbName = connectionString
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
}
