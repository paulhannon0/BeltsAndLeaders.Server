using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Users.CreateUser;
using BeltsAndLeaders.Server.Business.Commands.Users.CreateUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace BeltsAndLeaders.Server.Api.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;
        private readonly ICreateUserCommand createUserCommand;

        public UsersController(
            ILogger<UsersController> logger,
            ICreateUserCommand createUserCommand
        )
        {
            this.logger = logger;
            this.createUserCommand = createUserCommand;
        }

        [HttpPost("/users")]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateUserRequestBody requestModel
        )
        {
            var commandRequest = requestModel.ToCommandRequest();
            var commandResponse = await this.createUserCommand.ExecuteAsync(commandRequest);

            return Created($"/users/{commandResponse}", commandResponse);
        }
    }
}
