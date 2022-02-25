namespace StoryBooks.Libraries.Email
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using StoryBooks.Libraries.Email.Models;
    using StoryBooks.Libraries.Email.Services;

    public static class EmailConfigurationExtensions
    {
        public static IServiceCollection AddEmail(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                    .AddEmailSettings(configuration)
                    .AddTransient<IEmailSender, EmailSender>();

        private static IServiceCollection AddEmailSettings(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<EmailSettings>(
                    configuration.GetSection(nameof(EmailSettings)), 
                    config => config.BindNonPublicProperties = true);
    }
}
