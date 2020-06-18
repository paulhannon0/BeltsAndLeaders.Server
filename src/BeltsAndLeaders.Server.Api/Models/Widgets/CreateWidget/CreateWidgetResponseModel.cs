namespace BeltsAndLeaders.Server.Api.Models.Widgets.CreateWidget
{
    public class CreateWidgetResponseModel
    {
        public ulong Id { get; set; }

        public static CreateWidgetResponseModel FromBusinessModel(ulong id)
        {
            return new CreateWidgetResponseModel
            {
                Id = id
            };
        }
    }
}
