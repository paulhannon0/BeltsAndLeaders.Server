using System;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Users.GetUser;
using BeltsAndLeaders.Server.Common.Enums;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Users.GetUser
{
    [Binding]
    [Scope(Feature = "Get User")]
    public class GetUserSteps
    {
        private readonly TestHost testHost;
        private readonly UserDataHelper userDataHelper;
        private Guid validId;
        private readonly string name;
        private readonly string email;
        private readonly string specialistArea;
        private readonly DateTimeOffset championStartDate;

        public GetUserSteps(TestHost testHost, UserDataHelper userDataHelper)
        {
            this.testHost = testHost;
            this.userDataHelper = userDataHelper;
            this.name = "UserName";
            this.email = "user@test.com";
            this.specialistArea = "Security";

            /* 
                Conversion from/to Unix timestamp is necessary here to mimic the
                same operation the server does when inserting into the DB, resulting
                in a DateTimeOffset to 3 decimal places after the seconds rather than 6,
                so 2020-08-09 16:05:15.7954576+00:00 becomes 2020-08-09 16:05:15.795+00:00
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
        [Scope(Feature = "Get User")]
        public async Task BeforeScenario()
        {
            this.validId = await this.userDataHelper.CreateUserAsync
            (
                this.name,
                this.email,
                this.specialistArea,
                this.championStartDate
            );
        }

        [Given("a valid request path for the \'Get User\' endpoint")]
        public void GivenAValidRequestPathForTheGetUserEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Get User\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheGetUserEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath("invalid_id");
        }

        [Given("a request path for the \'Get User\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheGetUserEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(0);
        }

        [Then(@"the User record can be found in the response body")]
        public async Task ThenTheUserRecordCanBeFoundInTheResponseBody()
        {
            var user = await this.testHost.ExtractResponseBodyAsync<GetUserResponseModel>();

            // Assert.IsTrue(user.Id > 0);
            Assert.AreEqual(this.name, user.Name);
            Assert.AreEqual(this.email, user.Email);
            Assert.AreEqual(0, user.TotalMaturityPoints);
            Assert.AreEqual(BeltType.None, user.Belt);
            Assert.AreEqual(this.specialistArea, user.SpecialistArea);
            Assert.AreEqual(this.championStartDate, user.ChampionStartDate);
        }

        private void SetEndpointPath(object userId)
        {
            this.testHost.EndpointPath = $"/users/{userId}";
        }
    }
}
