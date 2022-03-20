namespace StoryBooks.Libraries.Email.Services;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
}