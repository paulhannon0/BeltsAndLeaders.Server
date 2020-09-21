using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Users.UpdateUser
{
    [Binding]
    [Scope(Feature = "Update User")]
    public class UpdateUserSteps
    {
        private readonly TestHost testHost;
        private readonly UserDataHelper userDataHelper;
        private Guid validId;
        private readonly string invalidId;
        private readonly Guid nonExistentId;
        private readonly string email;
        private readonly string updatedUserEmail;
        private readonly string name;
        private readonly string specialistArea;
        private readonly DateTimeOffset championStartDate;


        public UpdateUserSteps(TestHost testHost, UserDataHelper userDataHelper)
        {
            this.testHost = testHost;
            this.userDataHelper = userDataHelper;
            this.invalidId = "Invalid_ID";
            // this.nonExistentId = 0;
            this.updatedUserEmail = "updateduser@test.com";
            this.championStartDate = DateTime.Now;
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
        [Scope(Feature = "Update User")]
        public async Task BeforeScenario()
        {
            this.validId = await this.userDataHelper.CreateUserAsync(this.name, this.email, this.specialistArea, this.championStartDate);
        }

        [Given("a valid request path for the \'Update User\' endpoint")]
        public void GivenAValidRequestPathForTheUpdateUserEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a valid request body for the \'Update User\' endpoint")]
        public void GivenAValidRequestBodyForTheUpdateUserEndpoint()
        {
            this.testHost.RequestBody.Add("Email", this.updatedUserEmail);
            this.testHost.RequestBody.Add("Name", this.name);
            this.testHost.RequestBody.Add("SpecialistArea", this.specialistArea);
        }

        [Given("a request path for the \'Update User\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheUpdateUserEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath(this.invalidId);
        }

        [Given("a request path for the \'Update User\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheUpdateUserEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(this.nonExistentId);
        }

        [Then("the User record has been updated in the database")]
        public async Task ThenTheUserRecordHasBeenUpdatedInTheDatabase()
        {
            var record = await this.userDataHelper.GetUserAsync(this.validId);

            Assert.AreEqual(this.updatedUserEmail, record.Email);
        }
        private void SetEndpointPath(object userId)
        {
            this.testHost.EndpointPath = $"/users/{userId}";
        }
    }
}
