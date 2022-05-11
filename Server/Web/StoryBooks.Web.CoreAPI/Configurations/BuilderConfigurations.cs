namespace StoryBooks.Web.CoreAPI.Configurations;

using StoryBooks.Features.Authors;
using StoryBooks.Features.Identity;
using StoryBooks.Libraries.EventBus;
using StoryBooks.Web.Configurations;

internal static class BuilderConfigurations
{
    internal static WebApplicationBuilder WebConfiguration(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddWebConfiguration(builder.Configuration, builder.Environment)
                        .AddIdentityFeature(builder.Configuration)
                        .AddAuthorsFeature(builder.Configuration)
                        .AddInMemoryTransit(typeof(AuthorsConfigurationExtensions).Assembly);

        return builder;
    }
}
