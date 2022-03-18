namespace StoryBooks.Libraries.Email.Services;

using Microsoft.Extensions.Options;

using StoryBooks.Libraries.Email.Models;

using System.Threading.Tasks;

public class EmailService : IEmailService
{
    private readonly EmailSettings settings;
    private readonly IEmailSender emailSender;
    private readonly ITemplateRenderer templateRenderer;

    public EmailService(
        IOptions<EmailSettings> opts,
        IEmailSender emailSender,
        ITemplateRenderer templateRenderer)
    {
        this.settings = opts.Value;
        this.emailSender = emailSender;
        this.templateRenderer = templateRenderer;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        await this.emailSender.SendEmailAsync(to, subject, body);
    }

    public Task SendAsync<TData>(SendEmailModel<TData> emailModel)
        where TData : class
    {
        string modelName = typeof(TData).Name;
        string viewName = modelName.Replace(this.settings.Dev.ModelNameSuffix, string.Empty);

        return SendAsync(viewName, emailModel);
    }

    public async Task SendAsync<TData>(string viewName, SendEmailModel<TData> emailModel)
        where TData : class
    {
        var body = await this.templateRenderer.RenderAsync(viewName, emailModel.Model);
        await this.emailSender.SendEmailAsync(emailModel.To, emailModel.Subject, body);
    }
}