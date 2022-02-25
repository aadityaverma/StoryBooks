namespace StoryBooks.Libraries.Email.Models
{
    public class EmailSettings
    {
        public string SendGridKey { get; private set; } = default!;

        public string SenderAddress { get; private set; } = default!;

        public string SenderName { get; private set; } = default!;
    }
}