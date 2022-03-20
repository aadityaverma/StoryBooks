namespace StoryBooks.Features.Identity.Infrastructure.Services;

using AutoMapper;

using Microsoft.AspNetCore.Identity;

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
    private readonly IIdentityUrlProvider urlProvider;

    public IdentityEmailService(
        IEmailService emailSender,
        IMapper mapper,
        IIdentityUrlProvider urlProvider)
    {
        this.emailService = emailSender;
        this.mapper = mapper;
        this.urlProvider = urlProvider;
    }

    public async Task SendUserRegisteredEmail(User user, string token)
    {
        var bodyModel = this.mapper.Map<UserRegistrationEmailModel>(user);

        bodyModel.ServerUrl = urlProvider.CoreApiUrl;
        bodyModel.ConfirmUrl = urlProvider.ConfirmEmailLink(user.Id, token);

        var emailModel = new SendEmailModel<UserRegistrationEmailModel>(
            to: user.UserName,
            subject: Email.UserRegistrationSubject,
            model: bodyModel);

        await this.emailService.SendAsync(emailModel);
    }
}