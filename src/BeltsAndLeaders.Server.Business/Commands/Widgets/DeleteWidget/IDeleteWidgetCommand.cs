using BeltsAndLeaders.Server.Business.Models.Widgets.DeleteWidget;

namespace BeltsAndLeaders.Server.Business.Commands.Widgets.DeleteWidget
{
    public interface IDeleteWidgetCommand : ICommand<DeleteWidgetCommandRequestModel, ulong> { }
}
