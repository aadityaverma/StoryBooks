namespace StoryBooks.Libraries.Email
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using StoryBooks.Libraries.Email.Models;
    using StoryBooks.Libraries.Email.Renderers;
    using StoryBooks.Libraries.Email.Renderers.Razror;
    using StoryBooks.Libraries.Email.Services;

    public static class EmailConfigurationExtensions
    {
        public static IServiceCollection AddEmailWithFluid(
            this IServiceCollection services,
            IConfiguration configuration)
            => services.AddEmail(configuration, EmailServiceType.WithRazor);

        public static IServiceCollection AddEmailWithRazor(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddEmailSettings(configuration)
                    .AddSendGrid()
                    .AddRazorTemplates()
                    .AddTransient<IEmailService, EmailService>();

            return services;
        }

        private static IServiceCollection AddEmail(
            this IServiceCollection services,
            IConfiguration configuration,
            EmailServiceType type)
        {
            services.AddEmailSettings(configuration)
                    .AddSendGrid()
                    .AddTransient<IEmailService, EmailService>();

            switch (type)
            {
                case EmailServiceType.WithRazor:
                    services.AddRazorTemplates(); break;
                case EmailServiceType.WithFluid:
                    services.AddFluidTemplates(); break;
                default: services.AddRazorTemplates(); break;
            }

            return services;
        }

        private static IServiceCollection AddEmailSettings(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<EmailSettings>(
                    configuration.GetSection(nameof(EmailSettings)),
                    config => config.BindNonPublicProperties = true);

        private static IServiceCollection AddSendGrid(this IServiceCollection services)
            => services.AddTransient<IEmailSender, SendGridEmailSender>();

        private static IServiceCollection AddRazorTemplates(this IServiceCollection services)
            => services.AddSingleton<ITemplateRenderer, RazorTemlateRenderer>();

        private static IServiceCollection AddFluidTemplates(this IServiceCollection services)
            => services.AddSingleton<ITemplateRenderer, FluidTemplateRenderer>();
    }
}
