using System.Linq;

namespace StoryBooks.Features.Application;

public class Result : BaseResult
{
    public Result() : base()
    {
    }

    protected Result(ResultCode code, string generalMessage)
        : base(code, generalMessage)
    {
    }

    protected Result(ResultCode code, string generalMessage, IEnumerable<ResultError>? errors)
        : base(code, generalMessage, errors)
    {
    }

    public Result AddError(string key, string message)
    {
        return AddError(new ResultError(key, message));
    }

    public Result AddError(ResultError error)
    {
        base.AddBaseError(error);
        return this;
    }

    public Result AddErrors(IEnumerable<ResultError> errors)
    {
        base.AddBaseErrors(errors);
        return this;
    }

    public static Result Success(string message)
    {
        return new Result(ResultCode.Ok, message);
    }

    public static Result NotFound(string message)
    {
        return new Result(ResultCode.NotFound, message);
    }

    public static Result Fail(string message)
    {
        return new Result(ResultCode.BadRequest, message, null);
    }

    public static Result Fail(string message, ResultError error)
    {
        return new Result(ResultCode.BadRequest, message, new List<ResultError> { error });
    }

    public static Result Fail(string message, IEnumerable<ResultError> errors)
    {
        return new Result(ResultCode.BadRequest, message, errors);
    }

    public static Result Fail(string message, IDictionary<string, IEnumerable<string>> errors)
    {
        var errorList = errors.Select(p => (ResultError)p);
        return new Result(ResultCode.BadRequest, message, errorList);
    }
}
