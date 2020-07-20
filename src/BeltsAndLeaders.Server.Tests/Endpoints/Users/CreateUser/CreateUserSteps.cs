using System;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Users.CreateUser
{
    [Binding]
    [Scope(Feature = "Create User")]
    public class CreateUserSteps
    {
        private readonly TestHost testHost;
        private readonly MaturityCategoryDataHelper userDataHelper;
        private ulong newResourceId;

        public CreateUserSteps(TestHost testHost, MaturityCategoryDataHelper userDataHelper)
        {
            this.testHost = testHost;
            this.userDataHelper = userDataHelper;
        }

        [Given("a valid request path for the \'Create User\' endpoint")]
        public void GivenAValidRequestPathForTheCreateUserEndpoint()
        {
            this.testHost.EndpointPath = "/users";
        }

        [Given("a valid request body for the \'Create User\' endpoint")]
        public void GivenAValidRequestBodyForTheCreateUserEndpoint()
        {
            this.BuildValidRequestBody();
        }

        [Given("a request body for the \'Create User\' endpoint containing an invalid (.*) parameter")]
        public void GivenARequestBodyForTheCreateUserEndpointContainingAnInvalidParameter(string field)
        {
            this.BuildValidRequestBody();

            switch (field)
            {
                case "Name":
                    this.testHost.RequestBody["Name"] = 1;
                    break;

                case "Email":
                    this.testHost.RequestBody["Email"] = 1;
                    break;

                case "SpecialistArea":
                    this.testHost.RequestBody["SpecialistArea"] = 1;
                    break;

                case "ChampionStartDate":
                    this.testHost.RequestBody["ChampionStartDate"] = "this_is_not_a_date";
                    break;

                default:
                    break;
            }
        }

        [Given("a request body for the \'Create User\' endpoint with a missing (.*) parameter")]
        public void GivenARequestBodyForTheCreateUserEndpointWithAMissingParameter(string field)
        {
            this.BuildValidRequestBody();

            this.testHost.RequestBody[field] = null;
        }

        [Then("the Location response header contains the ID of the new resource")]
        public void ThenTheLocationResponseHeaderContainsTheIdOfTheNewResource()
        {
            var locationHeader = this.testHost.LastResponseMessage.Headers.Location;

            this.newResourceId = ulong.Parse
            (
                locationHeader
                    ?.ToString()
                    .Split("/")
                    .LastOrDefault()
            );

            Assert.AreNotEqual(0, this.newResourceId);
        }

        [Then("the User record has been inserted into the database")]
        public async Task ThenTheUserRecordHasBeenInsertedIntoTheDatabase()
        {
            var doesRecordExist = await this.userDataHelper.DoesRecordExist<UserRecord>(this.newResourceId);

            Assert.IsTrue(doesRecordExist);
        }

        private void BuildValidRequestBody()
        {
            this.testHost.RequestBody.Add("Name", Guid.NewGuid().ToString());
            this.testHost.RequestBody.Add("Email", Guid.NewGuid().ToString());
            this.testHost.RequestBody.Add("SpecialistArea", Guid.NewGuid().ToString());
            this.testHost.RequestBody.Add("ChampionStartDate", DateTimeOffset.Now.Subtract(TimeSpan.FromDays(7)));
        }
    }
}
