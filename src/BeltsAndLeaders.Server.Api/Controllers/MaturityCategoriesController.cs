using BeltsAndLeaders.Server.Api.Models.MaturityCategories.CreateMaturityCategory;
using BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetAllMaturityCategories;
// using BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetAllMaturityCategories;
using BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetMaturityCategory;
// using BeltsAndLeaders.Server.Api.Models.MaturityCategories.UpdateMaturityCategory;
using BeltsAndLeaders.Server.Business.Commands.MaturityCategories.CreateMaturityCategory;
using BeltsAndLeaders.Server.Business.Commands.MaturityCategories.DeleteMaturityCategory;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories.DeleteMaturityCategory;
// using BeltsAndLeaders.Server.Business.Commands.MaturityCategories.DeleteMaturityCategory;
// using BeltsAndLeaders.Server.Business.Commands.MaturityCategories.UpdateMaturityCategory;
// using BeltsAndLeaders.Server.Business.Models.MaturityCategories.DeleteMaturityCategory;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories.GetMaturityCategory;
using BeltsAndLeaders.Server.Business.Queries.MaturityCategories.GetAllMaturityCategories;
// using BeltsAndLeaders.Server.Business.Queries.MaturityCategories.GetAllMaturityCategories;
using BeltsAndLeaders.Server.Business.Queries.MaturityCategories.GetMaturityCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace BeltsAndLeaders.Server.Api.Controllers
{
    [ApiController]
    public class MaturityCategoriesController : ApiController
    {
        private readonly ILogger<MaturityCategoriesController> logger;
        private readonly ICreateMaturityCategoryCommand createMaturityCategoryCommand;
        // private readonly IUpdateMaturityCategoryCommand updateMaturityCategoryCommand;
        private readonly IDeleteMaturityCategoryCommand deleteMaturityCategoryCommand;
        private readonly IGetMaturityCategoryQuery getMaturityCategoryQuery;
        private readonly IGetAllMaturityCategoriesQuery getAllMaturityCategoriesQuery;

        public MaturityCategoriesController(
            ILogger<MaturityCategoriesController> logger,
            ICreateMaturityCategoryCommand createMaturityCategoryCommand,
        // IUpdateMaturityCategoryCommand updateMaturityCategoryCommand,
            IDeleteMaturityCategoryCommand deleteMaturityCategoryCommand,
            IGetMaturityCategoryQuery getMaturityCategoryQuery,
            IGetAllMaturityCategoriesQuery getAllMaturityCategoriesQuery
        )
        {
            this.logger = logger;
            this.createMaturityCategoryCommand = createMaturityCategoryCommand;
            // this.updateMaturityCategoryCommand = updateMaturityCategoryCommand;
            this.deleteMaturityCategoryCommand = deleteMaturityCategoryCommand;
            this.getMaturityCategoryQuery = getMaturityCategoryQuery;
            this.getAllMaturityCategoriesQuery = getAllMaturityCategoriesQuery;
        }

        [HttpPost("/maturity-categories")]
        [Consumes("application/json")]
        [SwaggerResponse((int)HttpStatusCode.Created)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(
            [FromBody] CreateMaturityCategoryRequestBody requestModel
        )
        {
            var commandRequest = requestModel.ToCommandRequest();
            var commandResponse = await this.createMaturityCategoryCommand.ExecuteAsync(commandRequest);

            return Created($"/maturity-categories/{commandResponse}", commandResponse);
        }

        [HttpGet("/maturity-categories/{Id}")]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetMaturityCategoryResponseModel>> Get(
            [FromRoute] ulong id
        )
        {
            var queryRequest = new GetMaturityCategoryQueryRequestModel { Id = id };
            var queryResponse = await this.getMaturityCategoryQuery.ExecuteAsync(queryRequest);

            return GetMaturityCategoryResponseModel.FromBusinessModel(queryResponse);
        }

        [HttpGet("/maturity-categories")]
        [Produces("application/json")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetAllMaturityCategoriesResponseModel>> Get()
        {
            var queryResponse = await this.getAllMaturityCategoriesQuery.ExecuteAsync();

            return GetAllMaturityCategoriesResponseModel.FromBusinessModel(queryResponse);
        }

        //         [HttpPut("/maturity-categories/{Id}")]
        //         [Consumes("application/json")]
        //         [SwaggerResponse((int)HttpStatusCode.NoContent)]
        //         [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        //         [SwaggerResponse((int)HttpStatusCode.NotFound)]
        //         public async Task<IActionResult> Update(
        //         [FromRoute] ulong id,
        //         [FromBody] UpdateMaturityCategoryRequestBody requestBody
        // )
        //         {
        //             var commandRequest = requestBody.ToCommandRequest(id);
        //             var commandResponse = await this.updateMaturityCategoryCommand.ExecuteAsync(commandRequest);

        //             return NoContent();
        //         }

        [HttpDelete("/maturity-categories/{Id}")]
        [SwaggerResponse((int)HttpStatusCode.NoContent)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(
            [FromRoute] ulong id
        )
        {
            var commandRequest = new DeleteMaturityCategoryCommandRequestModel { Id = id };
            await this.deleteMaturityCategoryCommand.ExecuteAsync(commandRequest);

            return NoContent();
        }
    }
}
