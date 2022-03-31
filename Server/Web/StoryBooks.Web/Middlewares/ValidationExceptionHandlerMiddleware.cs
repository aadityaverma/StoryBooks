namespace StoryBooks.Web.Middlewares;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using StoryBooks.Features.Application;
using StoryBooks.Features.Application.Exceptions;
using StoryBooks.Features.Domain.Exceptions;

using System;
using System.Threading.Tasks;

public class ValidationExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ValidationExceptionHandlerMiddleware(RequestDelegate next)
        => this.next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = exception switch
        {
            ModelValidationException validationException => 
                Result.Fail(exception.Message, validationException.Errors),
            EntityNotFoundException notFoundException => 
                Result.NotFound(notFoundException.Message),
            _ => Result.Fail(WebConstants.InvalidRequest, exception.Message),
        };

        context.Response.ContentType = WebConstants.ResponseType;
        context.Response.StatusCode = result.Code.Value;

        string response = SerializeObject(result.Errors);
        return context.Response.WriteAsync(response);
    }

    private static string SerializeObject(object obj)
        => JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy(true, true)
            }
        });
}

public static class ValidationExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseValidationExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<ValidationExceptionHandlerMiddleware>();
}