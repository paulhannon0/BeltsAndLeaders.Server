using System;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetAllMaturityLevels;
using BeltsAndLeaders.Server.Common.Enums;
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
        private Guid maturityLevelId;
        private Guid maturityCategoryId;
        private readonly BeltType beltLevel;
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
            this.beltLevel = BeltType.White;
            this.description = "Must have attended 3 security conferences.";
        }

        [BeforeScenario]
        [Scope(Feature = "Get All Maturity Levels")]
        public async Task BeforeScenario()
        {
            this.maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
            this.maturityLevelId = await this.maturityLevelDataHelper.CreateMaturityLevelAsync
            (
                this.maturityCategoryId,
                this.beltLevel,
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
            var maturityLevel = response.MaturityLevels.Find(l => l.Id == this.maturityLevelId);

            Assert.AreEqual(this.maturityCategoryId, maturityLevel.MaturityCategoryId);
            Assert.AreEqual(this.beltLevel, maturityLevel.BeltLevel);
            Assert.AreEqual(this.description, maturityLevel.Description);
        }
    }
}
