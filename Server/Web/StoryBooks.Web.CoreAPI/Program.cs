using StoryBooks.Web.CoreAPI.Configurations;

await WebApplication
    .CreateBuilder(args)
    .WebConfiguration()
    .Build()
    .ConfigureApp()
    .RunAsync();