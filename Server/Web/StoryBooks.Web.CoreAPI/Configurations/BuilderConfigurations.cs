namespace StoryBooks.Web.CoreAPI.Configurations
{
    using StoryBooks.Features.Identity;
    using StoryBooks.Libraries.Email;

    internal static class BuilderConfigurations
    {
        internal static WebApplicationBuilder WebConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddWebConfiguration(builder.Configuration)
                            .AddIdentityFeature(builder.Configuration);

            return builder;
        }

        private static IServiceCollection AddWebConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddEmail(configuration)
                    .AddSwaggerGen()
                    .AddEndpointsApiExplorer()
                    .AddControllers();

            return services;
        }
    }
}
