using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetAllMaturityLevels;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityLevels.GetAllMaturityLevels
{
    [Binding]
    [Scope(Feature = "Get All Maturity Levels")]
    public class GetAllMaturityLevelsSteps
    {
        private readonly TestHost testHost;
        private readonly MaturityLevelDataHelper maturityLevelDataHelper;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private ulong maturityCategoryId;
        private readonly byte maturityLevel;
        private readonly string description;

        public GetAllMaturityLevelsSteps(
            TestHost testHost,
            MaturityLevelDataHelper maturityLevelDataHelper,
            MaturityCategoryDataHelper maturityCategoryDataHelper
        )
        {
            this.testHost = testHost;
            this.maturityLevelDataHelper = maturityLevelDataHelper;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
            this.maturityLevel = 1;
            this.description = "Must have attended 3 security conferences.";
        }

        [BeforeScenario]
        [Scope(Feature = "Get All Maturity Levels")]
        public async Task BeforeScenario()
        {
            this.maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
            await this.maturityLevelDataHelper.CreateMaturityLevelAsync
            (
                this.maturityCategoryId,
                this.maturityLevel,
                this.description
            );
        }

        [Given("a valid request path for the \'Get All Maturity Levels\' endpoint")]
        public void GivenAValidRequestPathForTheGetAllMaturityLevelsEndpoint()
        {
            this.testHost.EndpointPath = $"/maturity-levels";
        }

        [Then(@"the MaturityLevel records can be found in the response body")]
        public async Task ThenTheMaturityLevelRecordsCanBeFoundInTheResponseBody()
        {
            var response = await this.testHost.ExtractResponseBodyAsync<GetAllMaturityLevelsResponseModel>();
            var maturityLevel = response.MaturityLevels.LastOrDefault();

            Assert.IsTrue(maturityLevel.Id > 0);
            Assert.AreEqual(this.maturityCategoryId, maturityLevel.MaturityCategoryId);
            Assert.AreEqual(this.maturityLevel, maturityLevel.MaturityLevel);
            Assert.AreEqual(this.description, maturityLevel.Description);
        }
    }
}
