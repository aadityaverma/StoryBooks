namespace StoryBooks.Features.Common
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    using StoryBooks.Features.Common.Application;
    using StoryBooks.Features.Common.Domain;
    using StoryBooks.Features.Common.Infrastructure;
    using StoryBooks.Features.Common.Presentation;

    using System.Reflection;
    using System.Text;

    public static class FeaturesConfigurationExtensions
    {
        public static IServiceCollection ConfigureFeature(
                this IServiceCollection services,
                IConfiguration configuration,
                Assembly featureAssembly)
                => services
                    .AddDomainLayer(featureAssembly)
                    .AddApplicationLayer(configuration, featureAssembly)
                    .AddInfrastructureLayer(featureAssembly)
                    .AddPresentationLayer();

        public static IServiceCollection ConfigureFeature<TContext>(
                this IServiceCollection services,
                IConfiguration configuration,
                Assembly featureAssembly)
            where TContext : DbContext
                => services
                    .AddDomainLayer(featureAssembly)
                    .AddApplicationLayer(configuration, featureAssembly)
                    .AddInfrastructureLayer<TContext>(configuration, featureAssembly)
                    .AddPresentationLayer();

        public static IServiceCollection AddAuthentication(
                this IServiceCollection services,
                IConfiguration configuration)
        {
            var secret = configuration.GetValue<string>("Authentication:Secret");
            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                //.AddFacebook(facebookOptions =>
                //{
                //    facebookOptions.AppId = configuration.GetValue<string>("Authentication:Facebook:AppId");
                //    facebookOptions.AppSecret = configuration.GetValue<string>("Authentication:Facebook:AppSecret");
                //})
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}