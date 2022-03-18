namespace StoryBooks.Web.Configurations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using StoryBooks.Features.Common.Application;
using StoryBooks.Features.Common.Infrastructure.Persistence;
using StoryBooks.Web.Middlewares;

public static class ApplicationConfigurations
{
    public static IApplicationBuilder ConfigureWebApp(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        var opts = serviceProvider.GetService<IOptions<ApplicationSettings>>();
        if (opts is null)
        {
            throw new ApplicationException(WebConstants.ApplicationSettingsNotConfigured);
        }

        string apiVersion = opts.Value.Version;
        string routePrefix = $"/api/{apiVersion}";
        string rootPath = env.ContentRootPath;
        app.UseValidationExceptionHandler()
            .UsePathBase(routePrefix)
            .UseHttpsRedirection()
            .UseRouting()
            .UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod())
            .UseAuthentication()
            .UseAuthorization()
            .UseStaticFiles()
            .UseEndpoints(endpoints => endpoints
                .MapControllers())
            .Initialize(serviceProvider);

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"./{apiVersion}/swagger.json", $"Api {apiVersion}");
            }
            );
            app.UseDeveloperExceptionPage();
        }

        return app;
    }

    private static IApplicationBuilder Initialize(this IApplicationBuilder app, IServiceProvider serviceProvider)
    {
        var services = serviceProvider.GetServices<IDataInitializer>();

        foreach (var initializer in services)
        {
            initializer.Initialize().GetAwaiter().GetResult();
        }

        return app;
    }
}