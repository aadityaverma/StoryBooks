namespace StoryBooks.Features.Common.Application.Commands;

public class IdModel<T>
{
    public IdModel()
    {
        this.Id = default!;
    }

    public IdModel(T id)
    {
        this.Id = id;
    }

    public T Id { get; set; }
}