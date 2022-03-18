namespace StoryBooks.Features.Domain.Exceptions;

public class EntityNotFoundException : BaseDomainException
{
    public EntityNotFoundException()
    { }

    public EntityNotFoundException(string name, object key)
        : this($"Entity '{name}' ({key}) was not found.")
    { }

    public EntityNotFoundException(string error) => this.Error = error;
}