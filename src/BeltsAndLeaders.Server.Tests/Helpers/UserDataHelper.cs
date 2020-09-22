using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Users.GetUser;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public class UserDataHelper : TestDataHelper
    {
        public UserDataHelper(TestHost testHost) : base(testHost) { }

        public async Task<Guid> CreateUserAsync(string name, string email, string specialistArea, DateTimeOffset? championStartDate = null)
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
            var locationFragments = responseMessage.Headers.Location.OriginalString.Split("/");

            return Guid.Parse(locationFragments[2]);
        }

        public async Task<GetUserResponseModel> GetUserAsync(Guid id)
        {
            var responseMessage = await this.TestHost.GetAsync($"/users/{id}");

            return JsonSerializer.Deserialize<GetUserResponseModel>
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
