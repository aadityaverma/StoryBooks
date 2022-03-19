namespace StoryBooks.Features.Identity.Presentation.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using StoryBooks.Features.Presentation.Controllers;
using StoryBooks.Features.Identity.Application.Commands.ChangePassword;

[Authorize]
[Route("Account/[controller]")]
[Obsolete("Use minimal API endpoints!")]
public class ManageController : ApiController
{
    [HttpPut(nameof(Password))]
    public async Task<ActionResult> Password(ChangePasswordCommand command)
        => await this.Send(command);
}