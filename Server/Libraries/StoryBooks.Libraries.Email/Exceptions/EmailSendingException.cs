namespace StoryBooks.Libraries.Email.Exceptions
{
    using StoryBooks.Libraries.Validation;

    public class EmailSendingException : ValidationException
    {
        public EmailSendingException()
        { }

        public EmailSendingException(string error) : base(error)
        { }
    }
}
