using BeltsAndLeaders.Server.Common.Enums;

namespace BeltsAndLeaders.Server.Business.Models.Widgets.CreateWidget
{
    public class CreateWidgetCommandRequestModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public byte MaturityLevel { get; set; }

        public BeltType Belt { get; set; }
    }
}
