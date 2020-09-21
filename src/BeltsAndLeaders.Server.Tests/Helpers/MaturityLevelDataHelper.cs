using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetMaturityLevel;
using BeltsAndLeaders.Server.Common.Enums;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public class MaturityLevelDataHelper : TestDataHelper
    {
        public MaturityLevelDataHelper(TestHost testHost) : base(testHost) { }

        public async Task<Guid> CreateMaturityLevelAsync(Guid maturityCategoryId, BeltType beltLevel, string description)
        {
            var requestBody = new Dictionary<string, object>()
            {
                { "MaturityCategoryId", maturityCategoryId },
                { "BeltLevel", beltLevel },
                { "Description", description }
            };

            var responseMessage = await this.TestHost.PostAsync("/maturity-levels", requestBody);

            return Guid.Parse(await responseMessage.Content.ReadAsStringAsync());
        }

        public async Task<GetMaturityLevelResponseModel> GetMaturityLevelAsync(Guid id)
        {
            var responseMessage = await this.TestHost.GetAsync($"/maturity-levels/{id}");

            return JsonSerializer.Deserialize<GetMaturityLevelResponseModel>
            (
                await responseMessage.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }
    }
}
