using BeltsAndLeaders.Server.Common.Enums;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityLevels.UpdateMaturityLevel
{
    [Binding]
    [Scope(Feature = "Update Maturity Level")]
    public class UpdateMaturityLevelSteps
    {
        private readonly TestHost testHost;
        private readonly MaturityLevelDataHelper maturityLevelDataHelper;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private Guid validId;
        private readonly string invalidId;
        private readonly Guid nonExistentId;
        private readonly string description;
        private readonly string updatedDescription;


        public UpdateMaturityLevelSteps(
            TestHost testHost,
            MaturityLevelDataHelper maturityLevelDataHelper,
            MaturityCategoryDataHelper maturityCategoryDataHelper
        )
        {
            this.testHost = testHost;
            this.maturityLevelDataHelper = maturityLevelDataHelper;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
            this.invalidId = "Invalid_ID";
            // this.nonExistentId = 0;
            this.description = "MaturityLevelDescription";
            this.updatedDescription = "UpdatedMaturityLevelDescription";
        }

        [BeforeScenario]
        [Scope(Feature = "Update Maturity Level")]
        public async Task BeforeScenario()
        {
            var maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
            this.validId = await this.maturityLevelDataHelper.CreateMaturityLevelAsync
            (
                maturityCategoryId,
                BeltType.White,
                this.description
            );
        }

        [Given("a valid request path for the \'Update Maturity Level\' endpoint")]
        public void GivenAValidRequestPathForTheUpdateMaturityLevelEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a valid request body for the \'Update Maturity Level\' endpoint")]
        public void GivenAValidRequestBodyForTheUpdateMaturityLevelEndpoint()
        {
            this.testHost.RequestBody.Add("Description", this.updatedDescription);
        }

        [Given("a request path for the \'Update Maturity Level\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheUpdateMaturityLevelEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath(this.invalidId);
        }

        [Given("a request path for the \'Update Maturity Level\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheUpdateMaturityLevelEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(this.nonExistentId);
        }

        [Then("the MaturityLevel record has been updated in the database")]
        public async Task ThenTheMaturityLevelRecordHasBeenUpdatedInTheDatabase()
        {
            var record = await this.maturityLevelDataHelper.GetMaturityLevelAsync(this.validId);

            Assert.AreEqual(this.updatedDescription, record.Description);
        }
        private void SetEndpointPath(object maturityLevelId)
        {
            this.testHost.EndpointPath = $"/maturity-levels/{maturityLevelId}";
        }
    }
}
