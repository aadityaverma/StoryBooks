namespace StoryBooks.Web.Configurations;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using StoryBooks.Libraries.Email;

using System.Text;

public static class BuilderConfigurations
{
    public static IServiceCollection AddWebConfiguration(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        string apiName = configuration.GetValue<string>("ApplicationSettings:ApiName");
        string apiVersion = configuration.GetValue<string>("ApplicationSettings:Version");

        services
            .AddCors()
            .AddAuthentication(configuration)
            .AddAuthorization(c =>
            {
                c.DefaultPolicy = new AuthorizationPolicyBuilder("Bearer")
                    .RequireAuthenticatedUser()
                    .Build();
            })
            .AddEmailWithFluid(configuration);

        if (environment.IsDevelopment())
        {
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(apiVersion, new OpenApiInfo
                    {
                        Title = apiName,
                        Version = apiVersion
                    });
                })
                .AddEndpointsApiExplorer();
        }

        return services;
    }

    private static IServiceCollection AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        string secret = configuration.GetValue<string>("Authentication:Secret");
        byte[] key = Encoding.ASCII.GetBytes(secret);

        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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