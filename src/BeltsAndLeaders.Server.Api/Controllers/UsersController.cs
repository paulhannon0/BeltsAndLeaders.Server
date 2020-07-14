using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Users.CreateUser;
using BeltsAndLeaders.Server.Api.Models.Users.GetUser;
using BeltsAndLeaders.Server.Api.Models.Users.UpdateUser;
using BeltsAndLeaders.Server.Business.Commands.Users.CreateUser;
using BeltsAndLeaders.Server.Business.Commands.Users.UpdateUser;
using BeltsAndLeaders.Server.Business.Models.Users.GetUser;
using BeltsAndLeaders.Server.Business.Queries.Users.GetUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace BeltsAndLeaders.Server.Api.Controllers
{
    [ApiController]
    public class UsersController : ApiController
    {
        private readonly ILogger<UsersController> logger;
        private readonly ICreateUserCommand createUserCommand;
        private readonly IUpdateUserCommand updateUserCommand;
        private readonly IGetUserQuery getUserQuery;

        public UsersController(
            ILogger<UsersController> logger,
            ICreateUserCommand createUserCommand,
            IUpdateUserCommand updateUserCommand,
            IGetUserQuery getUserQuery
        )
        {
            this.logger = logger;
            this.createUserCommand = createUserCommand;
            this.getUserQuery = getUserQuery;
            this.updateUserCommand = updateUserCommand;
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

        [HttpGet("/users/{Id}")]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetUserResponseModel>> Get(
            [FromRoute] ulong id
        )
        {
            var queryRequest = new GetUserQueryRequestModel { Id = id };
            var queryResponse = await this.getUserQuery.ExecuteAsync(queryRequest);

            return GetUserResponseModel.FromBusinessModel(queryResponse);
        }

        [HttpPut("/users/{Id}")]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(
        [FromRoute] ulong id,
        [FromBody] UpdateUserRequestBody requestBody
        )
        {
            var commandRequest = requestBody.ToCommandRequest(id);
            var commandResponse = await this.updateUserCommand.ExecuteAsync(commandRequest);

            return NoContent();
        }

        //[HttpDelete("/users/{Id}")]
        //[SwaggerResponse((int)HttpStatusCode.NoContent)]
        //[SwaggerResponse((int)HttpStatusCode.BadRequest)]
        //[SwaggerResponse((int)HttpStatusCode.NotFound)]
        //public async Task<IActionResult> Delete(
        //    [FromRoute] ulong id
        //)
        //{
        //    var commandRequest = new DeleteWidgetCommandRequestModel { Id = id };
        //    await this.deleteWidgetCommand.ExecuteAsync(commandRequest);

        //    return NoContent();
        //}
    }
}
