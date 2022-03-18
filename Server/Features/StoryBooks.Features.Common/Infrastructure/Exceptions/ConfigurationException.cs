namespace StoryBooks.Features.Common.Infrastructure.Exceptions;

using StoryBooks.Libraries.Validation;

public class ConfigurationException : ValidationException
{
    public ConfigurationException()
    { }

    public ConfigurationException(string error) => this.Error = error;
}