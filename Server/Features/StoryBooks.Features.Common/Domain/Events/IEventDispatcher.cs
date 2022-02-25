namespace StoryBooks.Features.Common.Domain.Events
{
    using System.Threading.Tasks;

    public interface IEventDispatcher
    {
        Task Dispatch(IDomainEvent domainEvent);
    }   
}