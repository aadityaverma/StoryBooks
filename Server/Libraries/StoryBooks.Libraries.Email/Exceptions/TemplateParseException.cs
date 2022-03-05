namespace StoryBooks.Libraries.Email.Exceptions
{
    using System.ComponentModel.DataAnnotations;

    public class TemplateParseException : ValidationException
    {
        public TemplateParseException()
        { }

        public TemplateParseException(string error) : base(error)
        { }
    }
}
