using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Achievements.CreateAchievement;
using BeltsAndLeaders.Server.Business.Commands.Achievements.CreateAchievement;
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

        public AchievementsController(
            ILogger<AchievementsController> logger,
            ICreateAchievementCommand createAchievementCommand
        )
        {
            this.logger = logger;
            this.createAchievementCommand = createAchievementCommand;
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
    }
}
