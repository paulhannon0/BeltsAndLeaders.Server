using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Widgets;
using BeltsAndLeaders.Server.Business.Models.Widgets.DeleteWidget;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.Widgets.DeleteWidget
{
    public class DeleteWidgetCommand : IDeleteWidgetCommand
    {
        private readonly IWidgetsRepository widgetsRepository;

        public DeleteWidgetCommand(IWidgetsRepository widgetsRepository)
        {
            this.widgetsRepository = widgetsRepository;
        }

        public async Task<ulong> ExecuteAsync(DeleteWidgetCommandRequestModel commandRequest)
        {
            var widget = await this.widgetsRepository.GetAsync(commandRequest.Id);

            if (widget == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"Widget (ID: {commandRequest.Id}) cannot be found.");
            }

            await this.widgetsRepository.DeleteAsync(commandRequest.Id);

            return commandRequest.Id;
        }
    }
}
