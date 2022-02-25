namespace StoryBooks.Features.Identity.Domain.Exceptions
{
    using StoryBooks.Features.Common.Domain.Exceptions;

    internal class InvalidUserNameException : BaseDomainException
    {
        public InvalidUserNameException()
        { }

        public InvalidUserNameException(string error) => this.Error = error;
    }
}
