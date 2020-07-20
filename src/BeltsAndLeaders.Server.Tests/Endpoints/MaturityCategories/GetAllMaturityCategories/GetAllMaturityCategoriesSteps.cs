using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetAllMaturityCategories;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityCategories.GetAllMaturityCategories
{
    [Binding]
    [Scope(Feature = "Get All Maturity Categories")]
    public class GetAllMaturityCategoriesSteps
    {
        private readonly TestHost testHost;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private readonly string name;

        public GetAllMaturityCategoriesSteps(TestHost testHost, MaturityCategoryDataHelper maturityCategoryDataHelper)
        {
            this.testHost = testHost;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
            this.name = "MaturityCategoryName";
        }

        [BeforeScenario]
        [Scope(Feature = "Get All Maturity Categories")]
        public async Task BeforeScenario()
        {
            await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync
            (
                this.name
            );
        }

        [Given("a valid request path for the \'Get All Maturity Categories\' endpoint")]
        public void GivenAValidRequestPathForTheGetAllMaturityCategoriesEndpoint()
        {
            this.testHost.EndpointPath = $"/maturity-categories";
        }

        [Then(@"the MaturityCategory records can be found in the response body")]
        public async Task ThenTheMaturityCategoryRecordsCanBeFoundInTheResponseBody()
        {
            var response = await this.testHost.ExtractResponseBodyAsync<GetAllMaturityCategoriesResponseModel>();
            var maturityCategory = response.MaturityCategories.LastOrDefault();

            Assert.IsTrue(maturityCategory.Id > 0);
            Assert.AreEqual(this.name, maturityCategory.Name);
        }
    }
}
