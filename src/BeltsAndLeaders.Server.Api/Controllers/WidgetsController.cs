using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BeltsAndLeaders.Server.Business.Queries.Widgets.GetWidget;
using BeltsAndLeaders.Server.Business.Commands.Widgets.CreateWidget;
using BeltsAndLeaders.Server.Api.Models.Widgets.GetWidget;
using BeltsAndLeaders.Server.Api.Models.Widgets.CreateWidget;
using BeltsAndLeaders.Server.Api.Models.Widgets.UpdateWidget;
using BeltsAndLeaders.Server.Business.Commands.Widgets.UpdateWidget;
using BeltsAndLeaders.Server.Business.Models.Widgets.GetWidget;
using BeltsAndLeaders.Server.Business.Models.Widgets.DeleteWidget;
using BeltsAndLeaders.Server.Business.Commands.Widgets.DeleteWidget;

namespace BeltsAndLeaders.Server.Api.Controllers
{
    [ApiController]
    public class WidgetsController : ControllerBase
    {
        private readonly ILogger<WidgetsController> logger;
        private readonly ICreateWidgetCommand createWidgetCommand;
        private readonly IGetWidgetQuery getWidgetQuery;
        private readonly IUpdateWidgetCommand updateWidgetCommand;
        private readonly IDeleteWidgetCommand deleteWidgetCommand;

        public WidgetsController(
            ILogger<WidgetsController> logger,
            ICreateWidgetCommand createWidgetCommand,
            IGetWidgetQuery getWidgetQuery,
            IUpdateWidgetCommand updateWidgetCommand,
            IDeleteWidgetCommand deleteWidgetCommand
        )
        {
            this.logger = logger;
            this.createWidgetCommand = createWidgetCommand;
            this.getWidgetQuery = getWidgetQuery;
            this.updateWidgetCommand = updateWidgetCommand;
            this.deleteWidgetCommand = deleteWidgetCommand;
        }

        [HttpPost("/widgets")]
        [Consumes("application/json")]
        public async Task<IActionResult> Create(
            [FromBody] CreateWidgetRequestBody requestModel
        )
        {
            var commandRequest = requestModel.ToCommandRequest();
            var commandResponse = await this.createWidgetCommand.ExecuteAsync(commandRequest);

            return Created($"/widgets/{commandResponse}", commandResponse);
        }

        [HttpGet("/widgets/{Id}")]
        [Produces("application/json")]
        public async Task<ActionResult<GetWidgetResponseModel>> Get(
            [FromRoute] ulong id
        )
        {
            var queryRequest = new GetWidgetQueryRequestModel { Id = id };
            var queryResponse = await this.getWidgetQuery.ExecuteAsync(queryRequest);

            return GetWidgetResponseModel.FromBusinessModel(queryResponse);
        }

        [HttpPut("/widgets/{Id}")]
        [Consumes("application/json")]
        public async Task<IActionResult> Update(
            [FromRoute] ulong id,
            [FromBody] UpdateWidgetRequestBody requestBody
        )
        {
            var commandRequest = requestBody.ToCommandRequest(id);
            var commandResponse = await this.updateWidgetCommand.ExecuteAsync(commandRequest);

            return NoContent();
        }

        [HttpDelete("/widgets/{Id}")]
        public async Task<IActionResult> Delete(
            [FromRoute] ulong id
        )
        {
            var commandRequest = new DeleteWidgetCommandRequestModel { Id = id };
            await this.deleteWidgetCommand.ExecuteAsync(commandRequest);

            return NoContent();
        }
    }
}
