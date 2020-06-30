using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Widgets.GetWidget;
using BeltsAndLeaders.Server.Data.Helpers;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public abstract class TestDataHelper
    {
        protected TestHost TestHost { get; set; }

        public TestDataHelper(TestHost testHost)
        {
            this.TestHost = testHost;
        }

        public async Task<bool> DoesRecordExist<T>(ulong id) where T : class
        {
            var record = await RepositoryHelper.GetByIdAsync<T>(id);

            return record != null;
        }

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
