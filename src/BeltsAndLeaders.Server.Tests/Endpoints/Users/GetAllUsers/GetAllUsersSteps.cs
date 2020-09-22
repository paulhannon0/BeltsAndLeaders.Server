using System;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Users.GetAllUsers;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Users.GetAllUsers
{
    [Binding]
    [Scope(Feature = "Get All Users")]
    public class GetAllUsersSteps
    {
        private readonly TestHost testHost;
        private readonly UserDataHelper userDataHelper;
        private Guid userId;
        private readonly string name;
        private readonly string email;
        private readonly string specialistArea;
        private readonly DateTimeOffset championStartDate;

        public GetAllUsersSteps(TestHost testHost, UserDataHelper userDataHelper)
        {
            this.testHost = testHost;
            this.userDataHelper = userDataHelper;
            this.name = "UserName";
            this.email = "user@test.com";
            this.specialistArea = "Security";

            /* 
                Conversion from/to Unix timestamp is necessary here to mimic the
                same operation the server does when inserting into the DB, resulting
                in a DateTimeOffset to 3 decimal places after the seconds rather than 6
            */
            this.championStartDate = DateTimeOffset.FromUnixTimeMilliseconds
            (
                DateTimeOffset.Now
                    .Subtract(TimeSpan.FromDays(7))
                    .ToUniversalTime()
                    .ToUnixTimeMilliseconds()
            );
        }

        [BeforeScenario]
        [Scope(Feature = "Get All Users")]
        public async Task BeforeScenario()
        {
            this.userId = await this.userDataHelper.CreateUserAsync
            (
                this.name,
                this.email,
                this.specialistArea,
                this.championStartDate
            );
        }

        [Given("a valid request path for the \'Get All Users\' endpoint")]
        public void GivenAValidRequestPathForTheGetAllUsersEndpoint()
        {
            this.testHost.EndpointPath = $"/users";
        }

        [Then(@"the User records can be found in the response body")]
        public async Task ThenTheUserRecordsCanBeFoundInTheResponseBody()
        {
            var response = await this.testHost.ExtractResponseBodyAsync<GetAllUsersResponseModel>();
            var user = response.Users.Find(u => u.Id == this.userId);

            Assert.AreEqual(this.name, user.Name);
            Assert.AreEqual(this.email, user.Email);
            Assert.AreEqual(this.specialistArea, user.SpecialistArea);
            Assert.AreEqual(this.championStartDate, user.ChampionStartDate);
        }
    }
}
