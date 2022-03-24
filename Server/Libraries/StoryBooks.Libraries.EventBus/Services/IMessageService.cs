namespace StoryBooks.Libraries.EventBus.Services
{
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task<bool> IsDuplicated(
            object messageData,
            string propertyFilter,
            object identifier);
    }
}
