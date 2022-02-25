namespace StoryBooks.Web.Configurations
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using StoryBooks.Features.Common.Infrastructure.Persistence;
    using StoryBooks.Web.Middlewares;

    public static class ApplicationConfigurations
    {
        public static IApplicationBuilder ConfigureWebApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseValidationExceptionHandler()
                .UseHttpsRedirection()
                .UseRouting()
                .UseCors(options => options
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod())
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints
                    .MapControllers())
                .Initialize();

            return app;
        }

        private static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var services = serviceProvider.GetServices<IDataInitializer>();

            foreach (var initializer in services)
            {
                initializer.Initialize().GetAwaiter().GetResult();
            }

            return app;
        }
    }
}
