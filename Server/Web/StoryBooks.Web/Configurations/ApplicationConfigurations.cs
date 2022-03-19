namespace StoryBooks.Web.Configurations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using StoryBooks.Features.Application;
using StoryBooks.Features.Infrastructure.Persistence;
using StoryBooks.Features.Presentation.Endpoints;
using StoryBooks.Web.Middlewares;

public static class ApplicationConfigurations
{
    public static IApplicationBuilder ConfigureWebApp(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        app.UseValidationExceptionHandler()
            .UseHttpsRedirection()
            .UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod())
            .UseAuthentication()
            .UseAuthorization()
            .UseStaticFiles();

        app.AddMinimalApiEndpoints(serviceProvider)
           .InitializeData(serviceProvider);
            
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        return app;
    }

    private static IApplicationBuilder AddMinimalApiEndpoints(
        this WebApplication app,
        IServiceProvider serviceProvider)
    {
        var opts = serviceProvider.GetService<IOptions<ApplicationSettings>>();
        if (opts is null)
        {
            throw new ApplicationException(WebConstants.ApplicationSettingsNotConfigured);
        }

        string apiVersion = opts.Value.Version;
        string routePrefix = $"/api/{apiVersion}";

        var endpointRegisters = serviceProvider.GetServices<IEndpointRegister>();

        foreach (var register in endpointRegisters)
        {
            register.AddEndpoints(app, routePrefix);
        }

        return app;
    }

    private static IApplicationBuilder InitializeData(
        this IApplicationBuilder app,
        IServiceProvider serviceProvider)
    {
        var services = serviceProvider.GetServices<IDataInitializer>();
        foreach (var initializer in services)
        {
            initializer.Initialize().GetAwaiter().GetResult();
        }

        return app;
    }
}