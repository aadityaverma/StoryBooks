﻿namespace StoryBooks.Libraries.Email.Services
{
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
        private const string ModelNameEnding = "EmailModel";

        private readonly EmailSettings settings;
        private readonly EmailAddress senderAddress;
        private readonly IEmailRenderer emailRenderer;

        public SendGridEmailSender(
            IEmailRenderer emailProvider,
            IOptions<EmailSettings> opts)
        {
            this.settings = opts.Value;
            ValidateSettings(this.settings);

            this.emailRenderer = emailProvider;
            this.senderAddress = new EmailAddress(settings.SenderAddress, settings.SenderName);
        }

        public async Task SendEmailAsync<TData>(SendEmailModel<TData> emailModel)
            where TData : class
        {
            string modelName = typeof(TData).Name;
            string viewName = modelName.Replace(ModelNameEnding, string.Empty);

            var body = await emailRenderer.RenderAsync(viewName, emailModel.Model);
            await SendEmailAsync(emailModel.To, emailModel.Subject, body);
        }

        public async Task SendEmailAsync<TData>(string templateViewName, SendEmailModel<TData> emailModel)
            where TData : class
        {
            var body = await emailRenderer.RenderAsync(templateViewName, emailModel);
            await SendEmailAsync(emailModel.To, emailModel.Subject, body);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            ValidateEmail(email, subject);

            var client = new SendGridClient(settings.SendGridKey);
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
        }

        private static void ValidateEmail(string email, string subject)
        {
            Guard.ForEmptyString<InvalidEmailException>(
                email, message: ErrorMessages.RecieverEmailRequired);

            Guard.ForEmptyString<InvalidEmailException>(
                subject, message: ErrorMessages.SubjectRequired);
        }

        private static void ValidateSettings(EmailSettings settings)
        {
            Guard.ForEmptyString<EmailConfigurationException>(
                settings.SendGridKey, message: ErrorMessages.SendGridKeyMissing);

            Guard.ForEmptyString<EmailConfigurationException>(
                settings.SenderAddress, message: ErrorMessages.SenderEmailMissing);

            Guard.ForEmptyString<EmailConfigurationException>(
                settings.SenderName, message: ErrorMessages.SenderNameMissing);
        }
    }
}