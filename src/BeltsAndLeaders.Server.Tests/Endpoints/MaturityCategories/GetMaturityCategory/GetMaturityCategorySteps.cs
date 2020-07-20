using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetMaturityCategory;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityCategories.GetMaturityCategory
{
    [Binding]
    [Scope(Feature = "Get Maturity Category")]
    public class GetMaturityCategorySteps
    {
        private readonly TestHost testHost;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private ulong validId;
        private readonly string name;

        public GetMaturityCategorySteps(TestHost testHost, MaturityCategoryDataHelper maturityCategoryDataHelper)
        {
            this.testHost = testHost;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
            this.name = "MaturityCategoryName";
        }

        [BeforeScenario]
        [Scope(Feature = "Get Maturity Category")]
        public async Task BeforeScenario()
        {
            this.validId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync
            (
                this.name
            );
        }

        [Given("a valid request path for the \'Get Maturity Category\' endpoint")]
        public void GivenAValidRequestPathForTheGetMaturityCategoryEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Get Maturity Category\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheGetMaturityCategoryEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath("invalid_id");
        }

        [Given("a request path for the \'Get Maturity Category\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheGetMaturityCategoryEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(0);
        }

        [Then(@"the MaturityCategory record can be found in the response body")]
        public async Task ThenTheMaturityCategoryRecordCanBeFoundInTheResponseBody()
        {
            var maturityCategory = await this.testHost.ExtractResponseBodyAsync<GetMaturityCategoryResponseModel>();

            Assert.IsTrue(maturityCategory.Id > 0);
            Assert.AreEqual(this.name, maturityCategory.Name);
        }

        private void SetEndpointPath(object maturityCategoryId)
        {
            this.testHost.EndpointPath = $"/maturity-categories/{maturityCategoryId}";
        }
    }
}
