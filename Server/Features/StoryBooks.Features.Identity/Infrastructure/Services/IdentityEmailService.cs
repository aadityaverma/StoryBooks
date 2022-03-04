namespace StoryBooks.Features.Identity.Infrastructure.Services
{
    using AutoMapper;

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

        public IdentityEmailService(
            IEmailService emailSender,
            IMapper mapper)
        {
            this.emailService = emailSender;
            this.mapper = mapper;
        }

        public async Task SendUserRegisteredEmail(User user)
        {
            var emailModel = new SendEmailModel<UserRegistrationEmailModel>(
                to: user.UserName,
                subject: Email.UserRegistrationSubject,
                model: this.mapper.Map<UserRegistrationEmailModel>(user));

            await this.emailService.SendAsync(emailModel);
        }
    }
}
