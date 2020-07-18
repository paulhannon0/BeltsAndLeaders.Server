using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetMaturityCategory;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public class MaturityCategoryDataHelper : TestDataHelper
    {
        public MaturityCategoryDataHelper(TestHost testHost) : base(testHost) { }

        public async Task<ulong> CreateMaturityCategoryAsync(string name)
        {
            var requestBody = new Dictionary<string, object>()
            {
                { "Name", name }
            };

            var responseMessage = await this.TestHost.PostAsync("/maturity-categories", requestBody);

            return ulong.Parse(await responseMessage.Content.ReadAsStringAsync());
        }

        public async Task<GetMaturityCategoryResponseModel> GetMaturityCategoryAsync(ulong id)
        {
            var responseMessage = await this.TestHost.GetAsync($"/maturity-categories/{id}");

            return JsonSerializer.Deserialize<GetMaturityCategoryResponseModel>
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
