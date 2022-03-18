namespace StoryBooks.Libraries.Email.Exceptions;

using System.ComponentModel.DataAnnotations;

public class TemplateRenderException : ValidationException
{
    public TemplateRenderException()
    { }

    public TemplateRenderException(string error) : base(error)
    { }
}