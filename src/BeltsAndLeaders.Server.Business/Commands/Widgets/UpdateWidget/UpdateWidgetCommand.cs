using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Widgets;
using BeltsAndLeaders.Server.Business.Models.Widgets.UpdateWidget;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.Widgets.UpdateWidget
{
    public class UpdateWidgetCommand : IUpdateWidgetCommand
    {
        private readonly IWidgetsRepository widgetsRepository;

        public UpdateWidgetCommand(IWidgetsRepository widgetsRepository)
        {
            this.widgetsRepository = widgetsRepository;
        }

        public async Task<ulong> ExecuteAsync(UpdateWidgetCommandRequestModel commandRequest)
        {
            var existingWidget = await this.widgetsRepository.GetAsync(commandRequest.Id);

            if (existingWidget == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"Widget (ID: {commandRequest.Id}) cannot be found.");
            }

            var widget = new Widget
            {
                Id = commandRequest.Id,
                Name = commandRequest.Name
            };

            await this.widgetsRepository.UpdateAsync(widget.ToTableRecord());

            return widget.Id;
        }
    }
}
