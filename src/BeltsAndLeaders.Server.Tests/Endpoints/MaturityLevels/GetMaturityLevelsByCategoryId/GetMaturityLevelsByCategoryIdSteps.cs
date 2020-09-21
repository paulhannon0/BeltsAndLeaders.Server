using System;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetMaturityLevelsByCategoryId;
using BeltsAndLeaders.Server.Common.Enums;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityLevels.GetMaturityLevelsByCategoryId
{
    [Binding]
    [Scope(Feature = "Get Maturity Levels by Category ID")]
    public class GetMaturityLevelsByCategoryIdSteps
    {
        private readonly TestHost testHost;
        private readonly MaturityLevelDataHelper maturityLevelDataHelper;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private Guid validId;
        private Guid maturityLevelId;
        private readonly BeltType beltLevel;
        private readonly string description;

        public GetMaturityLevelsByCategoryIdSteps(
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
        [Scope(Feature = "Get Maturity Levels by Category ID")]
        public async Task BeforeScenario()
        {
            this.validId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
            this.maturityLevelId = await this.maturityLevelDataHelper.CreateMaturityLevelAsync
            (
                this.validId,
                this.beltLevel,
                this.description
            );
        }

        [Given("a valid request path for the \'Get Maturity Levels by Category ID\' endpoint")]
        public void GivenAValidRequestPathForTheGetMaturityLevelsByCategoryIdEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Get Maturity Levels by Category ID\' endpoint with an invalid MaturityCategoryId parameter")]
        public void GivenARequestPathForTheGetMaturityLevelsByCategoryIdEndpointWitBeltsAndLeadersInvalidParameter()
        {
            this.SetEndpointPath("invalid_id");
        }

        [Given("a request path for the \'Get Maturity Levels by Category ID\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheGetMaturityLevelsByCategoryIdEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(0);
        }

        [Then(@"the MaturityLevel record can be found in the response body")]
        public async Task ThenTheMaturityLevelRecordCanBeFoundInTheResponseBody()
        {
            var body = await this.testHost.ExtractResponseBodyAsync<GetMaturityLevelsByCategoryIdResponseModel>();

            Assert.AreEqual(1, body.MaturityLevels.Count);

            var maturityLevel = body.MaturityLevels.FirstOrDefault();

            Assert.AreEqual(this.maturityLevelId, maturityLevel.Id);
            Assert.AreEqual(this.validId, maturityLevel.MaturityCategoryId);
            Assert.AreEqual(this.beltLevel, maturityLevel.BeltLevel);
            Assert.AreEqual(this.description, maturityLevel.Description);
        }

        private void SetEndpointPath(object maturityCategoryId)
        {
            this.testHost.EndpointPath = $"/maturity-categories/{maturityCategoryId}/maturity-levels";
        }
    }
}
