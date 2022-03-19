namespace StoryBooks.Features.Presentation.Extensions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using StoryBooks.Features.Application;

public static class ResultExtensions
{
    public static async Task<ActionResult<TData>> ToActionResult<TData>(this Task<TData> resultTask)
    {
        var result = await resultTask;

        if (result is null)
        {
            return new NotFoundResult();
        }

        return result;
    }

    public static async Task<ActionResult> ToActionResult(this Task<Result> resultTask)
    {
        var result = await resultTask;

        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        return new OkResult();
    }

    public static async Task<ActionResult<TData>> ToActionResult<TData>(this Task<Result<TData>> resultTask)
    {
        var result = await resultTask;
        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        if (result.Data is null)
        {
            return new BadRequestResult();
        }

        return result.Data;
    }

    public static async Task<IResult> ToIResult<TData>(this Task<TData> resultTask)
    {
        var data = await resultTask;
        if (data is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(data);
    }

    public static async Task<IResult> ToIResult(this Task<Result> resultTask)
    {
        var result = await resultTask;

        if (result.Code == ResultCode.Ok)
        {
            return Results.Ok(result.Message);
        }
       
        if (result.Code == ResultCode.Created)
        {
            return Results.Created(string.Empty, result.Message);
        }

        if (result.Code == ResultCode.BadRequest)
        {
            return Results.BadRequest(result.Errors);
        }

        if (result.Code == ResultCode.NotFound)
        {
            return Results.NotFound(result.Message);
        }

        if (result.Code == ResultCode.NotAuthorized)
        {
            return Results.Unauthorized();
        }

        return Results.Problem();
    }

    public static async Task<IResult> ToIResult<TData>(this Task<Result<TData>> resultTask)
    {
        var result = await resultTask;

        if (result.Code == ResultCode.Ok)
        {
            return Results.Ok(result.Data);
        }

        if (result.Code == ResultCode.Created)
        {
            return Results.Created(string.Empty, result.Data);
        }

        if (result.Code == ResultCode.BadRequest)
        {
            return Results.BadRequest(result.Errors);
        }

        if (result.Code == ResultCode.NotFound)
        {
            return Results.NotFound(result.Message);
        }

        if (result.Code == ResultCode.NotAuthorized)
        {
            return Results.Unauthorized();
        }

        return Results.Problem();
    }
}