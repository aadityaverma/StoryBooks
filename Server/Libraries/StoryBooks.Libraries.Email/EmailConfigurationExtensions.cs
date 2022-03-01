namespace StoryBooks.Libraries.Email
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using StoryBooks.Libraries.Email.IO;
    using StoryBooks.Libraries.Email.Models;
    using StoryBooks.Libraries.Email.Services;

    public static class EmailConfigurationExtensions
    {
        public static IServiceCollection AddEmail(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddRazorPages()
                .AddRazorRuntimeCompilation(opts =>
                {
                    opts.FileProviders.Clear();

                    string rootPath = configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
                    opts.FileProviders.Add(new EmbededResourcesFileProvider(rootPath));
                })
                .AddRazorOptions(opts =>
                {
                    var templatesLocation = configuration.GetValue<string>("EmailSettings:TemplatesLocation");
                    opts.ViewLocationFormats.Add($"{templatesLocation}/{{0}}/{{0}}Email.cshtml");
                    opts.ViewLocationFormats.Add($"{templatesLocation}/{{0}}/{{0}}.cshtml");
                    opts.ViewLocationFormats.Add($"{templatesLocation}/Shared/{{0}}.cshtml");
                });

            services
                .AddEmailSettings(configuration)
                .AddTransient<IEmailRenderer, RazorEmailRenderer>()
                .AddTransient<IEmailSender, SendGridEmailSender>();

            return services;
        }

        private static IServiceCollection AddEmailSettings(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .Configure<EmailSettings>(
                    configuration.GetSection(nameof(EmailSettings)), 
                    config => config.BindNonPublicProperties = true);
    }
}
