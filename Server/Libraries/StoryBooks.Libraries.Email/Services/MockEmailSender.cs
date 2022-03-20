namespace StoryBooks.Libraries.Email.Services;

public class MockEmailSender : IEmailSender
{
    public Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return Task.FromResult(true);
    }
}