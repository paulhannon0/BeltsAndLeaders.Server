using BeltsAndLeaders.Server.Business.Models.Widgets;
using BeltsAndLeaders.Server.Business.Models.Widgets.GetWidget;

namespace BeltsAndLeaders.Server.Business.Queries.Widgets.GetWidget
{
    public interface IGetWidgetQuery : IQuery<GetWidgetQueryRequestModel, Widget> { }
}
