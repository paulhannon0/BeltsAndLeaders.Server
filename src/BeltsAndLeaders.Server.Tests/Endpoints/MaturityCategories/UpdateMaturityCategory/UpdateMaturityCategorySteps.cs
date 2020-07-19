using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityCategories.UpdateMaturityCategory
{
    [Binding]
    [Scope(Feature = "Update Maturity Category")]
    public class UpdateMaturityCategorySteps
    {
        private readonly TestHost testHost;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private ulong validId;
        private readonly string invalidId;
        private readonly ulong nonExistentId;
        private readonly string name;
        private readonly string updatedName;


        public UpdateMaturityCategorySteps(TestHost testHost, MaturityCategoryDataHelper maturityCategoryDataHelper)
        {
            this.testHost = testHost;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
            this.invalidId = "Invalid_ID";
            this.nonExistentId = 0;
            this.name = "MaturityCategoryName";
            this.updatedName = "UpdatedMaturityCategoryName";
        }

        [BeforeScenario]
        [Scope(Feature = "Update Maturity Category")]
        public async Task BeforeScenario()
        {
            this.validId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync(this.name);
        }

        [Given("a valid request path for the \'Update Maturity Category\' endpoint")]
        public void GivenAValidRequestPathForTheUpdateMaturityCategoryEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a valid request body for the \'Update Maturity Category\' endpoint")]
        public void GivenAValidRequestBodyForTheUpdateMaturityCategoryEndpoint()
        {
            this.testHost.RequestBody.Add("Name", this.updatedName);
        }

        [Given("a request path for the \'Update Maturity Category\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheUpdateMaturityCategoryEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath(this.invalidId);
        }

        [Given("a request path for the \'Update Maturity Category\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheUpdateMaturityCategoryEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(this.nonExistentId);
        }

        [Then("the MaturityCategory record has been updated in the database")]
        public async Task ThenTheMaturityCategoryRecordHasBeenUpdatedInTheDatabase()
        {
            var record = await this.maturityCategoryDataHelper.GetMaturityCategoryAsync(this.validId);

            Assert.AreEqual(this.updatedName, record.Name);
        }
        private void SetEndpointPath(object maturityCategoryId)
        {
            this.testHost.EndpointPath = $"/maturity-categories/{maturityCategoryId}";
        }
    }
}
