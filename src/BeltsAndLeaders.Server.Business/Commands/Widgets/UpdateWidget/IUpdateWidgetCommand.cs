using BeltsAndLeaders.Server.Business.Models.Widgets.UpdateWidget;

namespace BeltsAndLeaders.Server.Business.Commands.Widgets.UpdateWidget
{
    public interface IUpdateWidgetCommand : ICommand<UpdateWidgetCommandRequestModel, ulong> { }
}
