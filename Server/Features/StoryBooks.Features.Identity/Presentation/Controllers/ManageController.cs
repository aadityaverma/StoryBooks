namespace StoryBooks.Features.Identity.Presentation.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using StoryBooks.Features.Common.Presentation.Controllers;
    using StoryBooks.Features.Identity.Application.Commands.ChangePassword;

    [Authorize]
    [Route("Account/[controller]")]
    public class ManageController : ApiController
    {
        [HttpPut(nameof(Password))]
        public async Task<ActionResult> Password(ChangePasswordCommand command)
            => await this.Send(command);
    }
}
