namespace StoryBooks.Features.Common.Presentation.Extensions
{
    using Microsoft.AspNetCore.Mvc;

    using StoryBooks.Features.Common.Application;

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
    }
}
