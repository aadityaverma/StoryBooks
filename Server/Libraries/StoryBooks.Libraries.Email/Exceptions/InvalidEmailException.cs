using StoryBooks.Libraries.Validation;

namespace StoryBooks.Libraries.Email.Exceptions
{
    public class InvalidEmailException : ValidationException
    {
        public InvalidEmailException() { }

        public InvalidEmailException(string error) : base(error) { }
    }
}
