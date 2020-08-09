using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Achievements.CreateAchievement;
using BeltsAndLeaders.Server.Api.Models.Achievements.GetAchievement;
using BeltsAndLeaders.Server.Api.Models.Achievements.GetAchievementsByUserId;
using BeltsAndLeaders.Server.Business.Commands.Achievements.CreateAchievement;
using BeltsAndLeaders.Server.Business.Models.Achievements.GetAchievement;
using BeltsAndLeaders.Server.Business.Models.Achievements.GetAchievementsByUserId;
using BeltsAndLeaders.Server.Business.Queries.Achievements.GetAchievement;
using BeltsAndLeaders.Server.Business.Queries.Achievements.GetAchievementsByUserId;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace BeltsAndLeaders.Server.Api.Controllers
{
    [ApiController]
    public class AchievementsController : BaseController
    {
        private readonly ILogger<AchievementsController> logger;
        private readonly ICreateAchievementCommand createAchievementCommand;
        private readonly IGetAchievementQuery getAchievementQuery;
        private readonly IGetAchievementsByUserIdQuery getAchievementsByUserIdQuery;

        public AchievementsController(
            ILogger<AchievementsController> logger,
            ICreateAchievementCommand createAchievementCommand,
            IGetAchievementQuery getAchievementQuery,
            IGetAchievementsByUserIdQuery getAchievementsByUserIdQuery
        )
        {
            this.logger = logger;
            this.createAchievementCommand = createAchievementCommand;
            this.getAchievementQuery = getAchievementQuery;
            this.getAchievementsByUserIdQuery = getAchievementsByUserIdQuery;
        }

        [HttpPost("/achievements")]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateAchievementRequestBody requestModel
        )
        {
            var commandRequest = requestModel.ToCommandRequest();
            var commandResponse = await this.createAchievementCommand.ExecuteAsync(commandRequest);

            return Created($"/achievements/{commandResponse}", commandResponse);
        }

        [HttpGet("/achievements/{Id}")]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetAchievementResponseModel>> Get(
            [FromRoute] ulong id
        )
        {
            var queryRequest = new GetAchievementQueryRequestModel { Id = id };
            var queryResponse = await this.getAchievementQuery.ExecuteAsync(queryRequest);

            return GetAchievementResponseModel.FromBusinessModel(queryResponse);
        }

        [HttpGet("/users/{UserId}/achievements")]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetAchievementsByUserIdResponseModel>> GetByUserId(
            [FromRoute] ulong userId
        )
        {
            var queryRequest = new GetAchievementsByUserIdQueryRequestModel { UserId = userId };
            var queryResponse = await this.getAchievementsByUserIdQuery.ExecuteAsync(queryRequest);

            return GetAchievementsByUserIdResponseModel.FromBusinessModel(queryResponse);
        }
    }
}
