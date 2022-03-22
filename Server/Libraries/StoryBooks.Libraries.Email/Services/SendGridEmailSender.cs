namespace StoryBooks.Libraries.Email.Services;

using Microsoft.Extensions.Options;

using SendGrid;
using SendGrid.Helpers.Mail;

using StoryBooks.Libraries.Email.Exceptions;
using StoryBooks.Libraries.Email.Models;
using StoryBooks.Libraries.Validation;

using System;

using static StoryBooks.Libraries.Email.EmailConstants;

public class SendGridEmailSender : IEmailSender
{
    private readonly EmailSettings settings;
    private readonly EmailAddress senderAddress;

    public SendGridEmailSender(IOptions<EmailSettings> opts)
    {
        this.settings = opts.Value;
        ValidateSettings(this.settings);

        this.senderAddress = new EmailAddress(settings.SenderAddress, settings.SenderName);
    }
    public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        ValidateEmail(email, subject);

        var client = new SendGridClient(settings.SenderApiKey);
        var to = new EmailAddress(email);

        var msg = MailHelper.CreateSingleEmail(senderAddress, to, subject, htmlMessage.StripHtmlTags(), htmlMessage);
        msg.SetClickTracking(false, false);

        var response = await client.SendEmailAsync(msg);
        Guard.ForNull<Response, EmailSendingException>(response, message: ErrorMessages.NoReponse);

        if (!response.IsSuccessStatusCode)
        {
            var responseBody = await response.Body.ReadAsStringAsync();
            throw new EmailSendingException(responseBody);
        }

        return response.IsSuccessStatusCode;
    }

    private static void ValidateEmail(string email, string subject)
    {
        Guard.ForEmptyString<InvalidEmailException>(
            email, message: ErrorMessages.ReceiverEmailRequired);

        Guard.ForEmptyString<InvalidEmailException>(
            subject, message: ErrorMessages.SubjectRequired);
    }

    private static void ValidateSettings(EmailSettings settings)
    {
        Guard.ForEmptyString<EmailConfigurationException>(
            settings.SenderApiKey, message: ErrorMessages.ApiKeyMissing);

        Guard.ForEmptyString<EmailConfigurationException>(
            settings.SenderAddress, message: ErrorMessages.SenderEmailMissing);

        Guard.ForEmptyString<EmailConfigurationException>(
            settings.SenderName, message: ErrorMessages.SenderNameMissing);
    }
}