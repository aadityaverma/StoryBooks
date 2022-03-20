namespace StoryBooks.Features.Application.Services
{
    public interface IUrlProvider
    {
        string ClientUrl { get; }

        string CoreApiUrl { get; }
    }
}
