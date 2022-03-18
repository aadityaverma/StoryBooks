namespace StoryBooks.Features.Common.Domain.Entities;

public class Image : Entity<string>
{
    public Image(string name, byte[] content)
    {
        this.Name = name;
        this.Content = content;
    }

    public string Name { get; private set; }

    public byte[] Content { get; private set; }
}