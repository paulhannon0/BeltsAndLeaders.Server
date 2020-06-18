using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Widgets;
using BeltsAndLeaders.Server.Business.Models.Widgets.CreateWidget;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.Widgets.CreateWidget
{
    public class CreateWidgetCommand : ICreateWidgetCommand
    {
        private readonly IWidgetsRepository widgetsRepository;

        public CreateWidgetCommand(IWidgetsRepository widgetsRepository)
        {
            this.widgetsRepository = widgetsRepository;
        }

        public async Task<ulong> ExecuteAsync(CreateWidgetCommandRequestModel commandRequest)
        {
            var widget = new Widget
            {
                Name = commandRequest.Name
            };

            return await this.widgetsRepository.CreateAsync(widget.ToTableRecord());
        }
    }
}
