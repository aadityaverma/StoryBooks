namespace StoryBooks.Libraries.Email.Exceptions;

using StoryBooks.Libraries.Validation;

public class InvalidEmailException : ValidationException
{
    public InvalidEmailException() { }

    public InvalidEmailException(string error) : base(error) { }
}