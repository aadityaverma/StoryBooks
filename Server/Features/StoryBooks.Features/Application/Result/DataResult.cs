namespace StoryBooks.Features.Application;

public class Result<TData> : BaseResult
{
    public Result() : base()
    {
    }

    protected Result(ResultCode code, string generalMessage, TData data)
        : base(code, generalMessage)
    {
        this.Data = data;
    }

    protected Result(ResultCode code, string generalMessage, IEnumerable<ResultError>? errors)
        : base(code, generalMessage, errors)
    {
    }

    public TData? Data { get; protected set; }

    public Result<TData> AddError(string key, string message)
    {
        return AddError(new ResultError(key, message));
    }

    public Result<TData> AddError(ResultError error)
    {
        base.AddBaseError(error);
        return this;
    }

    public Result<TData> AddErrors(IEnumerable<ResultError> errors)
    {
        base.AddBaseErrors(errors);
        return this;
    }

    public static Result<TData> Success(string message, TData data)
    {
        return new Result<TData>(ResultCode.Ok, message, data);
    }

    public static Result<TData> NotFound(string message)
    {
        return new Result<TData>(ResultCode.NotFound, message, null);
    }

    public static Result<TData> Fail(string message)
    {
        return Fail(message, new ResultError(string.Empty, message));
    }

    public static Result<TData> Fail(string message, ResultError error)
    {
        return new Result<TData>(ResultCode.BadRequest, message, new List<ResultError> { error });
    }

    public static Result<TData> Fail(string message, IEnumerable<ResultError> errors)
    {
        return new Result<TData>(ResultCode.BadRequest, message, errors);
    }
}