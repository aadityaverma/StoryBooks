namespace StoryBooks.Features.Identity.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using StoryBooks.Features.Common.Application.Commands;
    using StoryBooks.Features.Common.Presentation.Controllers;
    using StoryBooks.Features.Identity.Application.Commands.LoginUser;
    using StoryBooks.Features.Identity.Application.Commands.RegisterUser;

    using System.Threading.Tasks;

    public class AccountController : ApiController
    {
        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult<RegisterUserSuccessModel>> Get()
        //    => await this.Send();

        [HttpPost]
        public async Task<ActionResult<IdModel<string>>> Post(RegisterUserCommand command)
            => await this.Send(command);

        //[HttpPut]
        //[Authorize]
        //public async Task<ActionResult<RegisterUserSuccessModel>> Put(UpdateUserCommand command)
        //    => await this.Send(command);


        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginUserSuccessModel>> Login(LoginUserCommand command)
            => await this.Send(command);
    }
}
