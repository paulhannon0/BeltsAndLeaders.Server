using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityLevels.DeleteMaturityLevel
{
    [Binding]
    [Scope(Feature = "Delete Maturity Level")]
    public class DeleteMaturityLevelSteps
    {
        private readonly TestHost testHost;
        private readonly MaturityLevelDataHelper maturityLevelDataHelper;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private ulong validId;
        private readonly string invalidId;
        private readonly ulong nonExistentId;

        public DeleteMaturityLevelSteps(
            TestHost testHost,
            MaturityLevelDataHelper maturityLevelDataHelper,
            MaturityCategoryDataHelper maturityCategoryDataHelper
        )
        {
            this.testHost = testHost;
            this.maturityLevelDataHelper = maturityLevelDataHelper;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
            this.invalidId = "invalid_id";
            this.nonExistentId = 0;
        }

        [BeforeScenario]
        [Scope(Feature = "Delete MaturityLevel")]
        public async Task BeforeScenario()
        {
            var maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
            this.validId = await this.maturityLevelDataHelper.CreateMaturityLevelAsync
            (
                maturityCategoryId,
                1,
                "Must have attended 3 security conferences."
            );
        }

        [Given("a valid request path for the \'Delete Maturity Level\' endpoint")]
        public void GivenAValidRequestPathForTheDeleteMaturityLevelEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Delete Maturity Level\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheDeleteMaturityLevelEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath(this.invalidId);
        }
        [Given("a request path for the \'Delete Maturity Level\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheDeleteMaturityLevelEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(this.nonExistentId);
        }

        [Then(@"the MaturityLevel record has been deleted from the database")]
        public async Task ThenTheMaturityLevelRecordHasBeenDeletedFromTheDatabase()
        {
            var doesRecordExist = await this.maturityLevelDataHelper.DoesRecordExist<MaturityLevelRecord>(this.validId);

            Assert.IsFalse(doesRecordExist);
        }

        private void SetEndpointPath(object maturityLevelId)
        {
            this.testHost.EndpointPath = $"/maturity-levels/{maturityLevelId}";
        }
    }
}
