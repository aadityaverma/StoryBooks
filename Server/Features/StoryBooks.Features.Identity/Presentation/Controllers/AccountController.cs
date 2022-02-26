﻿namespace StoryBooks.Features.Identity.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using StoryBooks.Features.Common.Application.Commands;
    using StoryBooks.Features.Common.Presentation.Controllers;
    using StoryBooks.Features.Identity.Application.Commands.LoginUser;
    using StoryBooks.Features.Identity.Application.Commands.RegisterUser;
    using StoryBooks.Features.Identity.Application.Commands.UpdateDetails;
    using StoryBooks.Features.Identity.Application.Queries.PersonalAccount;

    using System.Threading.Tasks;

    public class AccountController : ApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDetailsModel>> Get()
            => await this.Send(new GetPersonalDetailsQuery());

        [HttpPost]
        public async Task<ActionResult<IdModel<string>>> Post(RegisterUserCommand command)
            => await this.Send(command);

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Put(UpdateUserDetailsCommand command)
            => await this.Send(command);

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginUserSuccessModel>> Login(LoginUserCommand command)
            => await this.Send(command);
    }
}
