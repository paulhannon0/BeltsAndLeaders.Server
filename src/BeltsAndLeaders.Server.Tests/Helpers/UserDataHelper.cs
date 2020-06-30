using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Users.GetUser;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public class UserDataHelper : TestDataHelper
    {
        public UserDataHelper(TestHost testHost) : base(testHost) { }

        public async Task<ulong> CreateUserAsync(string name, string email, string specialistArea, DateTimeOffset? championStartDate)
        {
            var requestBody = new Dictionary<string, object>()
            {
                { "Name", name },
                { "Email", email },
                { "SpecialistArea", specialistArea }
            };

            if (championStartDate != null)
            {
                requestBody.Add("ChampionStartDate", championStartDate);
            }

            var responseMessage = await this.TestHost.PostAsync("/users", requestBody);

            return ulong.Parse(await responseMessage.Content.ReadAsStringAsync());
        }

        public async Task<GetUserResponseModel> GetUserAsync(ulong id)
        {
            var responseMessage = await this.TestHost.GetAsync($"/users/{id}");

            return JsonSerializer.Deserialize<GetUserResponseModel>
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
