namespace StoryBooks.Features.Application;

using StoryBooks.Features.Domain.Entities;

using System.Collections.Generic;
using System.Linq;

public abstract class BaseResult
{
    protected readonly IDictionary<string, ResultError> _errors;

    protected BaseResult()
    {
        this.Message = string.Empty;
        _errors = new Dictionary<string, ResultError>();
    }

    protected BaseResult(ResultCode code, string generalMessage) : this()
    {
        this.Code = code;
        this.Message = generalMessage;
        this.Succeeded = IsSuccessCode(code);
    }

    protected BaseResult(ResultCode code, string generalMessage, IEnumerable<ResultError>? errors) : this()
    {
        this.Code = code;
        this.Message = generalMessage;
        this.Succeeded = IsSuccessCode(code);
        AddErrors(errors ?? new List<ResultError>());
    }

    public BaseResult AddError(string key, string message)
    {
        return AddError(new ResultError(key, message));
    }

    public BaseResult AddError(ResultError error)
    {
        if (!_errors.ContainsKey(error.Key))
        {
            _errors.Add(error.Key, error);
        }
        else
        {
            _errors[error.Key].Errors.AddRange(error.Errors);
        }

        return this;
    }

    public BaseResult AddErrors(IEnumerable<ResultError> errors)
    {
        foreach (var error in errors)
        {
            AddError(error);
        }

        return this;
    }

    /// <summary>
    /// Result code of the action. It's easier to be same as Http status codes
    /// </summary>
    public ResultCode Code { get; protected set; } = default!;

    /// <summary>
    /// Main message from the executed action
    /// </summary>
    public string Message { get; protected set; }

    /// <summary>
    /// Shows if the action is completed successfully
    /// </summary>
    public bool Succeeded { get; protected set; }

    /// <summary>
    /// Shows if any error is present from the action
    /// </summary>
    public bool HasError => _errors.IsNotEmpty();

    /// <summary>
    /// Dictionary with all errors occurred when executing the action.
    /// The key is the name of the failed property with list of error messages
    /// </summary>
    public IEnumerable<ResultError> Errors => _errors.Select(e => e.Value);

    protected static bool IsSuccessCode(ResultCode code)
    {
        return code == ResultCode.Ok;
    }
}

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

}

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

    public Result<TData> SetError(string errorMessage)
    {
        this.Code = ResultCode.BadRequest;
        this.AddError(string.Empty, errorMessage);
        this.Succeeded = false;
        this.Data = default;
        return this;
    }

    public Result<TData> SetSuccess(string generalMessage, TData data)
    {
        this.Code = ResultCode.Ok;
        this.Message = generalMessage;
        this.Succeeded = true;
        this.Data = data;
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
        return new Result<TData>(ResultCode.BadRequest, message, null);
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

public class ResultError
{
    public ResultError()
    {
        this.Key = string.Empty;
        Errors = new List<string>();
    }

    public ResultError(string key, string error)
    {
        this.Key = key;
        this.Errors = new List<string> { error };
    }

    public ResultError(string key, IList<string> errors)
    {
        this.Key = key;
        this.Errors = errors;
    }

    public string Key { get; set; }

    public IList<string> Errors { get; set; }
}

public class ResultCode : Enumeration
{
    public static readonly ResultCode Ok = new(200, nameof(Ok));
    public static readonly ResultCode BadRequest = new(400, nameof(BadRequest));
    public static readonly ResultCode NotAuthorized = new(403, nameof(NotAuthorized));
    public static readonly ResultCode NotFound = new(404, nameof(NotFound));

    private ResultCode(int value, string name) : base(value, name)
    {
    }
}