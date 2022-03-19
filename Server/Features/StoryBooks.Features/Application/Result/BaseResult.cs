namespace StoryBooks.Features.Application;

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

        if (errors != null)
        {
            AddBaseErrors(errors);
        }
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

    protected void AddBaseError(string key, string message)
    {
        AddBaseError(new ResultError(key, message));
    }

    protected void AddBaseError(ResultError error)
    {
        if (!_errors.ContainsKey(error.Key))
        {
            _errors.Add(error.Key, error);
        }
        else
        {
            _errors[error.Key].Errors.AddRange(error.Errors);
        }
    }

    protected void AddBaseErrors(IEnumerable<ResultError> errors)
    {
        foreach (var error in errors)
        {
            AddBaseError(error);
        }
    }
}