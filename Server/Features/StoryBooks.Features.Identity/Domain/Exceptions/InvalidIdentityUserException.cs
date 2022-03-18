namespace StoryBooks.Features.Identity.Domain.Exceptions;

using StoryBooks.Features.Common.Domain.Exceptions;

internal class InvalidIdentityUserException : BaseDomainException
{
    public InvalidIdentityUserException()
    { }

    public InvalidIdentityUserException(string error) => this.Error = error;
}