namespace StoryBooks.Libraries.Email.IO
{
    using StoryBooks.Libraries.Validation;

    public class ResourceDuplicationException : ValidationException
    {
        public ResourceDuplicationException()
        { }

        public ResourceDuplicationException(string error) => this.Error = error;
    }
}
