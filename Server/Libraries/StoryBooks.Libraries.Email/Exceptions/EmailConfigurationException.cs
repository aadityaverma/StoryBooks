namespace StoryBooks.Libraries.Email.Exceptions;

using StoryBooks.Libraries.Validation;

public class EmailConfigurationException : ValidationException
{
    public EmailConfigurationException() { }

    public EmailConfigurationException(string error) : base(error) { }
}