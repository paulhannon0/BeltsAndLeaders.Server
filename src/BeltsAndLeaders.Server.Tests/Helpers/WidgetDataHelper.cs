using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Widgets.GetWidget;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public class WidgetDataHelper : TestDataHelper
    {
        public WidgetDataHelper(TestHost testHost) : base(testHost) { }

        public async Task<ulong> CreateWidgetAsync(string name)
        {
            var responseMessage = await this.TestHost.PostAsync
            (
                "/widgets",
                new Dictionary<string, object>()
                {
                    { "Name", name }
                }
            );

            return ulong.Parse(await responseMessage.Content.ReadAsStringAsync());
        }

        public async Task<GetWidgetResponseModel> GetWidgetAsync(ulong id)
        {
            var responseMessage = await this.TestHost.GetAsync($"/widgets/{id}");

            return JsonSerializer.Deserialize<GetWidgetResponseModel>
            (
                await responseMessage.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }
    }
}
