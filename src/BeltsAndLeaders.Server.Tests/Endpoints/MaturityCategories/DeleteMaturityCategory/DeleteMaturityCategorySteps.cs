using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityCategories.DeleteMaturityCategory
{
    [Binding]
    [Scope(Feature = "Delete Maturity Category")]
    public class DeleteMaturityCategorySteps
    {
        private readonly TestHost testHost;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private ulong validId;
        private readonly string invalidId;
        private readonly ulong nonExistentId;

        public DeleteMaturityCategorySteps(TestHost testHost, MaturityCategoryDataHelper maturityCategoryDataHelper)
        {
            this.testHost = testHost;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
            this.invalidId = "invalid_id";
            this.nonExistentId = 0;
        }

        [BeforeScenario]
        [Scope(Feature = "Delete MaturityCategory")]
        public async Task BeforeScenario()
        {
            this.validId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("MaturityCategoriesName");
        }

        [Given("a valid request path for the \'Delete Maturity Category\' endpoint")]
        public void GivenAValidRequestPathForTheDeleteMaturityCategoryEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Delete Maturity Category\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheDeleteMaturityCategoryEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath(this.invalidId);
        }
        [Given("a request path for the \'Delete Maturity Category\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheDeleteMaturityCategoryEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(this.nonExistentId);
        }

        [Then(@"the MaturityCategory record has been deleted from the database")]
        public async Task ThenTheMaturityCategoryRecordHasBeenDeletedFromTheDatabase()
        {
            var doesRecordExist = await this.maturityCategoryDataHelper.DoesRecordExist<MaturityCategoryRecord>(this.validId);

            Assert.IsFalse(doesRecordExist);
        }

        private void SetEndpointPath(object maturityCategoryId)
        {
            this.testHost.EndpointPath = $"/maturity-categories/{maturityCategoryId}";
        }
    }
}
