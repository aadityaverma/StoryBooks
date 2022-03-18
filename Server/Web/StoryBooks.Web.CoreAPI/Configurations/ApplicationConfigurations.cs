namespace StoryBooks.Web.CoreAPI.Configurations;

using StoryBooks.Web.Configurations;

internal static class ApplicationConfigurations
{
    public static WebApplication ConfigureApp(this WebApplication app)
    {
        app.ConfigureWebApp(app.Environment);
        return app;
    }
}