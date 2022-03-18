namespace StoryBooks.Libraries.Validation;

public class ValidationException : Exception
{
    public ValidationException()
    {
    }

    public ValidationException(string error)
    {
        this.error = error;
    }

    private string? error;

    public string Error
    {
        get => this.error ?? base.Message;
        set => this.error = value;
    }
}