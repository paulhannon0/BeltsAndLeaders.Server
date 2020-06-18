using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Widgets;
using BeltsAndLeaders.Server.Business.Models.Widgets.GetWidget;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.Widgets.GetWidget
{
    public class GetWidgetQuery : IGetWidgetQuery
    {
        private readonly IWidgetsRepository widgetsRepository;

        public GetWidgetQuery(IWidgetsRepository widgetsRepository)
        {
            this.widgetsRepository = widgetsRepository;
        }

        public async Task<Widget> ExecuteAsync(GetWidgetQueryRequestModel queryRequest)
        {
            var widget = await this.widgetsRepository.GetAsync(queryRequest.Id);

            if (widget == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"Widget (ID: {queryRequest.Id}) cannot be found.");
            }

            return Widget.FromTableRecord(widget);
        }
    }
}
