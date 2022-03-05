namespace StoryBooks.Libraries.Email.Exceptions
{
    using System.ComponentModel.DataAnnotations;

    public class TemplateNotFoundException : ValidationException
    {
        public TemplateNotFoundException()
        { }

        public TemplateNotFoundException(string error) : base(error)
        { }
    }
}
