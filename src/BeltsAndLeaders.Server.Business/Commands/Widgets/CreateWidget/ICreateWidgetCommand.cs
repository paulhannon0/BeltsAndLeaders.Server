using BeltsAndLeaders.Server.Business.Models.Widgets.CreateWidget;

namespace BeltsAndLeaders.Server.Business.Commands.Widgets.CreateWidget
{
    public interface ICreateWidgetCommand : ICommand<CreateWidgetCommandRequestModel, ulong> { }
}
