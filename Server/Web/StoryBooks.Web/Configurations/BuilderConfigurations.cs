namespace StoryBooks.Web.Configurations
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    using StoryBooks.Libraries.Email;

    public static class BuilderConfigurations
    {
        public static IServiceCollection AddWebConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var apiName = configuration.GetValue<string>("ApplicationSettings:ApiName");
            var apiVersion = configuration.GetValue<string>("ApplicationSettings:Version");

            services
                .AddEmailWithFluid(configuration)
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(apiVersion, new OpenApiInfo
                    {
                        Title = apiName,
                        Version = apiVersion,
                        Description = $"Prepend '/api/{apiVersion}/' for each swagger generated endpoint. This is the global route prefix that is not taken into account by swagger in .net 6."
                    });
                })
                .AddEndpointsApiExplorer()
                .AddControllers();

            return services;
        }
    }
}
