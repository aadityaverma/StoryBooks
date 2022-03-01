namespace StoryBooks.Libraries.Email.Exceptions
{
    using System.ComponentModel.DataAnnotations;

    public class EmailTemplateNotFoundException : ValidationException
    {
        public EmailTemplateNotFoundException()
        { }

        public EmailTemplateNotFoundException(string error) : base(error)
        { }
    }
}
