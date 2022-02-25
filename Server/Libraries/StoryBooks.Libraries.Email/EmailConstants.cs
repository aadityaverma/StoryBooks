namespace StoryBooks.Libraries.Email
{
    internal class EmailConstants
    {
        internal class ErrorMessages
        {
            public const string RecieverEmailRequired = "Reciever email is required!";
            public const string SubjectRequired = "Email subject is required!";
            public const string NoReponse = "No response from the mail server!";
            public const string SendGridKeyMissing = "SendGrid authentication key is not provided!";
            public const string SenderEmailMissing = "Sender email address is not provided!";
            public const string SenderNameMissing = "Sender name is not provided!";
        }
    }
}
