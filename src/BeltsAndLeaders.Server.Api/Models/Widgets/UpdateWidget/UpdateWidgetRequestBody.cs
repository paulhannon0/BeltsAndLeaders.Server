using BeltsAndLeaders.Server.Business.Models.Widgets.UpdateWidget;

namespace BeltsAndLeaders.Server.Api.Models.Widgets.UpdateWidget
{
    public class UpdateWidgetRequestBody
    {
        public string Name { get; set; }

        public UpdateWidgetCommandRequestModel ToCommandRequest(ulong id)
        {
            return new UpdateWidgetCommandRequestModel
            {
                Id = id,
                Name = this.Name
            };
        }
    }
}
