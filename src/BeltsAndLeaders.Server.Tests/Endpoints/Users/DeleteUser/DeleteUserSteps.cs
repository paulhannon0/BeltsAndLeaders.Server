using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Users.DeleteUser
{
    [Binding]
    [Scope(Feature = "Delete User")]
    public class DeleteUserSteps
    {
        private readonly TestHost testHost;
        private readonly UserDataHelper userDataHelper;
        private Guid validId;
        private readonly string invalidId;
        private readonly Guid nonExistentId;

        public DeleteUserSteps(TestHost testHost, UserDataHelper userDataHelper)
        {
            this.testHost = testHost;
            this.userDataHelper = userDataHelper;
            this.invalidId = "invalid_id";
            this.nonExistentId = Guid.NewGuid();
        }

        [BeforeScenario]
        [Scope(Feature = "Delete User")]
        public async Task BeforeScenario()
        {
            this.validId = await this.userDataHelper.CreateUserAsync("UsersName", "UsersEmail", "Security", DateTime.Now);
        }

        [Given("a valid request path for the \'Delete User\' endpoint")]
        public void GivenAValidRequestPathForTheDeleteUserEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Delete User\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheDeleteUserEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath(this.invalidId);
        }
        [Given("a request path for the \'Delete User\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheDeleteUserEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(this.nonExistentId);
        }

        [Then(@"the User record has been deleted from the database")]
        public async Task ThenTheUserRecordHasBeenDeletedFromTheDatabase()
        {
            var doesRecordExist = await this.userDataHelper.DoesRecordExist<UserRecord>(this.validId);

            Assert.IsFalse(doesRecordExist);
        }

        private void SetEndpointPath(object userId)
        {
            this.testHost.EndpointPath = $"/users/{userId}";
        }
    }
}
