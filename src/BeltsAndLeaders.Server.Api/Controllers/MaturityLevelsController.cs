using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetAllMaturityLevels;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.CreateMaturityLevel;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetMaturityLevel;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.GetMaturityLevel;
using BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetAllMaturityLevels;
using BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetMaturityLevel;
using BeltsAndLeaders.Server.Business.Commands.MaturityLevels.CreateMaturityLevel;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.DeleteMaturityLevel;
using BeltsAndLeaders.Server.Business.Commands.MaturityLevels.DeleteMaturityLevel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.UpdateMaturityLevel;
using BeltsAndLeaders.Server.Business.Commands.MaturityLevels.UpdateMaturityLevel;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetMaturityLevelsByCategoryId;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.GetMaturityLevelsByCategoryId;
using BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetMaturityLevelsByCategoryId;
using System;

namespace BeltsAndLeaders.Server.Api.Controllers
{
    [ApiController]
    public class MaturityLevelsController : BaseController
    {
        private readonly ILogger<MaturityLevelsController> logger;
        private readonly ICreateMaturityLevelCommand createMaturityLevelCommand;
        private readonly IUpdateMaturityLevelCommand updateMaturityLevelCommand;
        private readonly IDeleteMaturityLevelCommand deleteMaturityLevelCommand;
        private readonly IGetMaturityLevelQuery getMaturityLevelQuery;
        private readonly IGetAllMaturityLevelsQuery getAllMaturityLevelsQuery;
        private readonly IGetMaturityLevelsByCategoryIdQuery getMaturityLevelsByCategoryIdQuery;

        public MaturityLevelsController(
            ILogger<MaturityLevelsController> logger,
            ICreateMaturityLevelCommand createMaturityLevelCommand,
            IUpdateMaturityLevelCommand updateMaturityLevelCommand,
            IDeleteMaturityLevelCommand deleteMaturityLevelCommand,
            IGetMaturityLevelQuery getMaturityLevelQuery,
            IGetAllMaturityLevelsQuery getAllMaturityLevelsQuery,
            IGetMaturityLevelsByCategoryIdQuery getMaturityLevelsByCategoryIdQuery
        )
        {
            this.logger = logger;
            this.createMaturityLevelCommand = createMaturityLevelCommand;
            this.updateMaturityLevelCommand = updateMaturityLevelCommand;
            this.deleteMaturityLevelCommand = deleteMaturityLevelCommand;
            this.getMaturityLevelQuery = getMaturityLevelQuery;
            this.getAllMaturityLevelsQuery = getAllMaturityLevelsQuery;
            this.getMaturityLevelsByCategoryIdQuery = getMaturityLevelsByCategoryIdQuery;
        }

        [HttpPost("/maturity-levels")]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateMaturityLevelRequestBody requestModel
        )
        {
            var commandRequest = requestModel.ToCommandRequest();
            var commandResponse = await this.createMaturityLevelCommand.ExecuteAsync(commandRequest);

            return Created($"/maturity-levels/{commandResponse}", commandResponse);
        }

        [HttpGet("/maturity-levels/{Id}")]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetMaturityLevelResponseModel>> Get(
            [FromRoute] Guid id
        )
        {
            var queryRequest = new GetMaturityLevelQueryRequestModel { Id = id };
            var queryResponse = await this.getMaturityLevelQuery.ExecuteAsync(queryRequest);

            return GetMaturityLevelResponseModel.FromBusinessModel(queryResponse);
        }

        [HttpGet("/maturity-levels")]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetAllMaturityLevelsResponseModel>> Get()
        {
            var queryResponse = await this.getAllMaturityLevelsQuery.ExecuteAsync();

            return GetAllMaturityLevelsResponseModel.FromBusinessModel(queryResponse);
        }

        [HttpGet("/maturity-categories/{MaturityCategoryId}/maturity-levels")]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetMaturityLevelsByCategoryIdResponseModel>> GetByCategoryId(
            [FromRoute] Guid maturityCategoryId
        )
        {
            var queryRequest = new GetMaturityLevelsByCategoryIdQueryRequestModel { CategoryId = maturityCategoryId };
            var queryResponse = await this.getMaturityLevelsByCategoryIdQuery.ExecuteAsync(queryRequest);

            return GetMaturityLevelsByCategoryIdResponseModel.FromBusinessModel(queryResponse);
        }

        [HttpPut("/maturity-levels/{Id}")]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateMaturityLevelRequestBody requestBody
        )
        {
            var commandRequest = requestBody.ToCommandRequest(id);
            var commandResponse = await this.updateMaturityLevelCommand.ExecuteAsync(commandRequest);

            return NoContent();
        }

        [HttpDelete("/maturity-levels/{Id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute] Guid id
        )
        {
            var commandRequest = new DeleteMaturityLevelCommandRequestModel { Id = id };
            await this.deleteMaturityLevelCommand.ExecuteAsync(commandRequest);

            return NoContent();
        }
    }
}
