namespace StoryBooks.Libraries.Email
{
    using Fluid;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using StoryBooks.Libraries.Email.Models;
    using StoryBooks.Libraries.Email.Renderers.FluidEngine;
    using StoryBooks.Libraries.Email.Renderers.RazrorEngine;
    using StoryBooks.Libraries.Email.Services;

    public static class EmailConfigurationExtensions
    {
        public static IServiceCollection AddEmailWithFluid(
            this IServiceCollection services,
            IConfiguration configuration)
            => services.AddEmail(configuration, EmailServiceType.WithFluid);

        public static IServiceCollection AddEmailWithRazor(
            this IServiceCollection services,
            IConfiguration configuration)
            => services.AddEmail(configuration, EmailServiceType.WithRazor);

        private static IServiceCollection AddEmail(
            this IServiceCollection services,
            IConfiguration configuration,
            EmailServiceType type)
        {
            services.AddEmailSettings(configuration)
                    .AddEmailLayout(configuration)
                    .AddMemoryCache()
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

        private static IServiceCollection AddEmailLayout(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<LayoutModel>(cfg =>
                {
                    cfg.SetClientUrl(configuration.GetValue<string>("ApplicationSettings:Urls:ClientUrl"));
                });

        private static IServiceCollection AddSendGrid(this IServiceCollection services)
            => services.AddTransient<IEmailSender, SendGridEmailSender>();

        private static IServiceCollection AddMockSender(this IServiceCollection services)
            => services.AddTransient<IEmailSender, MockEmailSender>();

        private static IServiceCollection AddRazorTemplates(this IServiceCollection services)
            => services.AddSingleton<ITemplateRenderer, RazorTemlateRenderer>();

        private static IServiceCollection AddFluidTemplates(this IServiceCollection services)
        {
            return services.AddSingleton<FluidParser>()
                           .AddTransient<ITemplateRenderer, FluidTemplateRenderer>();
        }
    }
}
