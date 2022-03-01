namespace StoryBooks.Libraries.Email.Services
{
    using StoryBooks.Libraries.Email.Models;

    public interface IEmailSender
    {
        Task SendEmailAsync<TData>(SendEmailModel<TData> emailModel)
            where TData : class;

        Task SendEmailAsync<TData>(string templateViewName, SendEmailModel<TData> emailModel)
            where TData : class;

        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
