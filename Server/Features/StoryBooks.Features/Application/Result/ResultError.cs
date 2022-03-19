namespace StoryBooks.Features.Application;

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
