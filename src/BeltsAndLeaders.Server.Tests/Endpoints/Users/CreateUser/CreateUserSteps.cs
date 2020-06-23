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
        private readonly TestDataHelper testDataHelper;
        private readonly string validName;
        private readonly int invalidName;
        private readonly string validEmail;
        private readonly int invalidEmail;
        private readonly string validSpecialistArea;
        private readonly int invalidSpecialistArea;
        private readonly DateTimeOffset validChampionStartDate;
        private readonly string invalidChampionStartDate;
        private ulong newResourceId;

        public CreateUserSteps(TestHost testHost, TestDataHelper testDataHelper)
        {
            this.testHost = testHost;
            this.testDataHelper = testDataHelper;
            this.validName = Guid.NewGuid().ToString();
            this.invalidName = 1;
            this.validEmail = Guid.NewGuid().ToString();
            this.invalidEmail = 1;
            this.validSpecialistArea = Guid.NewGuid().ToString();
            this.invalidSpecialistArea = 1;
            this.validChampionStartDate = DateTimeOffset.Now.Subtract(TimeSpan.FromDays(7));
            this.invalidChampionStartDate = "this_is_not_a_date";
        }

        [Given("a valid request path for the \'Create User\' endpoint")]
        public void GivenAValidRequestPathForTheCreateUserEndpoint()
        {
            this.testHost.EndpointPath = "/users";
        }

        [Given("a valid request body for the \'Create User\' endpoint")]
        public void GivenAValidRequestBodyForTheCreateUserEndpoint()
        {
            this.testHost.RequestBody.Add("Name", this.validName);
            this.testHost.RequestBody.Add("Email", this.validEmail);
            this.testHost.RequestBody.Add("SpecialistArea", this.validSpecialistArea);
            this.testHost.RequestBody.Add("ChampionStartDate", this.validChampionStartDate);
        }

        [Given("a request body for the \'Create User\' endpoint containing an invalid (.*) parameter")]
        public void GivenARequestBodyForTheCreateUserEndpointContainingAnInvalidParameter(string field)
        {
            switch (field)
            {
                case "Name":
                    this.testHost.RequestBody.Add("Name", this.invalidName);
                    break;

                case "Email":
                    this.testHost.RequestBody.Add("Email", this.invalidEmail);
                    break;

                case "SpecialistArea":
                    this.testHost.RequestBody.Add("SpecialistArea", this.invalidSpecialistArea);
                    break;

                case "ChampionStartDate":
                    this.testHost.RequestBody.Add("ChampionStartDate", this.invalidChampionStartDate);
                    break;

                default:
                    break;
            }
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
            var doesRecordExist = await this.testDataHelper.DoesRecordExist<UserRecord>(this.newResourceId);

            Assert.IsTrue(doesRecordExist);
        }
    }
}
