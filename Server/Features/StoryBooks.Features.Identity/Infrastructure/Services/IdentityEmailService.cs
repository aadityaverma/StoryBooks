namespace StoryBooks.Features.Identity.Infrastructure.Services;

using AutoMapper;

using Microsoft.Extensions.Options;

using StoryBooks.Features.Application;
using StoryBooks.Features.Identity.Application.Services;
using StoryBooks.Features.Identity.Domain.Entities;
using StoryBooks.Features.Identity.Resources.EmailTemplates.UserRegistration;
using StoryBooks.Libraries.Email.Models;
using StoryBooks.Libraries.Email.Services;

using System.Threading.Tasks;

using static StoryBooks.Features.Identity.Infrastructure.IdentityInfrastructureConstants;

public class IdentityEmailService : IIdentityEmailService
{
    private readonly IEmailService emailService;
    private readonly IMapper mapper;
    private readonly ApplicationSettings settings;

    public IdentityEmailService(
        IEmailService emailSender,
        IMapper mapper,
        IOptions<ApplicationSettings> opts)
    {
        this.emailService = emailSender;
        this.mapper = mapper;
        this.settings = opts.Value;
    }

    public async Task SendUserRegisteredEmail(User user)
    {
        var bodyModel = this.mapper.Map<UserRegistrationEmailModel>(user);
        bodyModel.ServerUrl = settings.URLs.CoreApiUrl;
        bodyModel.ConfirmUrl = $"{bodyModel.ServerUrl}/api/{settings.Version}/account/confirm/";

        var emailModel = new SendEmailModel<UserRegistrationEmailModel>(
            to: user.UserName,
            subject: Email.UserRegistrationSubject,
            model: bodyModel);

        await this.emailService.SendAsync(emailModel);
    }
}