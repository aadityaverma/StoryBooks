namespace StoryBooks.Web.CoreAPI.Configurations
{
    using StoryBooks.Features.Identity;
    using StoryBooks.Web.Configurations;

    using System.Reflection;

    internal static class BuilderConfigurations
    {
        internal static WebApplicationBuilder WebConfiguration(this WebApplicationBuilder builder)
        {
            var resources = typeof(IdentityConfigurationExtensions).Assembly.GetManifestResourceNames();

            builder.Services.AddWebConfiguration(builder.Configuration)
                            .AddIdentityFeature(builder.Configuration);

            return builder;
        }
    }
}
