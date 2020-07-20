using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public class MaturityLevelDataHelper : TestDataHelper
    {
        public MaturityLevelDataHelper(TestHost testHost) : base(testHost) { }

        public async Task<ulong> CreateMaturityLevelAsync(ulong maturityCategoryId, byte maturityLevel, string description)
        {
            var requestBody = new Dictionary<string, object>()
            {
                { "MaturityCategoryId", maturityCategoryId },
                { "MaturityLevel", maturityLevel },
                { "Description", description },
            };

            var responseMessage = await this.TestHost.PostAsync("/maturity-levels", requestBody);

            return ulong.Parse(await responseMessage.Content.ReadAsStringAsync());
        }

        // public async Task<GetMaturityLevelResponseModel> GetMaturityLevelAsync(ulong id)
        // {
        //     var responseMessage = await this.TestHost.GetAsync($"/maturity-categories/{id}");

        //     return JsonSerializer.Deserialize<GetMaturityLevelResponseModel>
        //     (
        //         await responseMessage.Content.ReadAsStringAsync(),
        //         new JsonSerializerOptions
        //         {
        //             PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //         }
        //     );
        // }
    }
}
