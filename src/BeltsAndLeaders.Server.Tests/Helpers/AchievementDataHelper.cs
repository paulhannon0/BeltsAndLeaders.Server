using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public class AchievementDataHelper : TestDataHelper
    {
        public AchievementDataHelper(TestHost testHost) : base(testHost) { }

        public async Task<Guid> CreateAchievementAsync(
            Guid userId,
            Guid maturityLevelId,
            DateTimeOffset achievementDate,
            string comment

        )
        {
            var requestBody = new Dictionary<string, object>()
            {
                { "UserId", userId },
                { "MaturityLevelId", maturityLevelId },
                { "AchievementDate", achievementDate },
                { "Comment", comment }
            };

            var responseMessage = await this.TestHost.PostAsync("/achievements", requestBody);

            return Guid.Parse(await responseMessage.Content.ReadAsStringAsync());
        }

        // public async Task<GetAchievementResponseModel> GetAchievementAsync(Guid id)
        // {
        //     var responseMessage = await this.TestHost.GetAsync($"/achievements/{id}");

        //     return JsonSerializer.Deserialize<GetAchievementResponseModel>
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
