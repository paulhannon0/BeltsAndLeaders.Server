using System;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetMaturityLevel;
using BeltsAndLeaders.Server.Common.Enums;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityLevels.GetMaturityLevel
{
    [Binding]
    [Scope(Feature = "Get Maturity Level")]
    public class GetMaturityLevelSteps
    {
        private readonly TestHost testHost;
        private readonly MaturityLevelDataHelper maturityLevelDataHelper;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private Guid validId;
        private Guid maturityCategoryId;
        private readonly BeltType beltLevel;
        private readonly string description;

        public GetMaturityLevelSteps(
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
        [Scope(Feature = "Get Maturity Level")]
        public async Task BeforeScenario()
        {
            this.maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
            this.validId = await this.maturityLevelDataHelper.CreateMaturityLevelAsync
            (
                this.maturityCategoryId,
                this.beltLevel,
                this.description
            );
        }

        [Given("a valid request path for the \'Get Maturity Level\' endpoint")]
        public void GivenAValidRequestPathForTheGetMaturityLevelEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Get Maturity Level\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheGetMaturityLevelEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath("invalid_id");
        }

        [Given("a request path for the \'Get Maturity Level\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheGetMaturityLevelEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(0);
        }

        [Then(@"the MaturityLevel record can be found in the response body")]
        public async Task ThenTheMaturityLevelRecordCanBeFoundInTheResponseBody()
        {
            var maturityLevel = await this.testHost.ExtractResponseBodyAsync<GetMaturityLevelResponseModel>();

            // Assert.IsTrue(maturityLevel.Id > 0);
            Assert.AreEqual(this.maturityCategoryId, maturityLevel.MaturityCategoryId);
            Assert.AreEqual(this.beltLevel, maturityLevel.BeltLevel);
            Assert.AreEqual(this.description, maturityLevel.Description);
        }

        private void SetEndpointPath(object maturityLevelId)
        {
            this.testHost.EndpointPath = $"/maturity-levels/{maturityLevelId}";
        }
    }
}
