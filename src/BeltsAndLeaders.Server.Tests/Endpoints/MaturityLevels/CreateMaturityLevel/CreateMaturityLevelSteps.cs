using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Common.Enums;
using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityLevels.CreateMaturityLevel
{
    [Binding]
    [Scope(Feature = "Create Maturity Level")]
    public class CreateMaturityLevelSteps
    {
        private readonly TestHost testHost;
        private readonly MaturityLevelDataHelper maturityLevelDataHelper;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private ulong newResourceId;
        private ulong maturityCategoryId;

        public CreateMaturityLevelSteps(
            TestHost testHost,
            MaturityLevelDataHelper maturityLevelDataHelper,
            MaturityCategoryDataHelper maturityCategoryDataHelper
        )
        {
            this.testHost = testHost;
            this.maturityLevelDataHelper = maturityLevelDataHelper;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
        }

        [BeforeScenario]
        [Scope(Feature = "Create Maturity Level")]
        public async Task BeforeScenario()
        {
            this.maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
        }

        [Given("a valid request path for the \'Create Maturity Level\' endpoint")]
        public void GivenAValidRequestPathForTheCreateMaturityLevelEndpoint()
        {
            this.testHost.EndpointPath = "/maturity-levels";
        }

        [Given("a valid request body for the \'Create Maturity Level\' endpoint")]
        public void GivenAValidRequestBodyForTheCreateMaturityLevelEndpoint()
        {
            this.BuildValidRequestBody();
        }

        [Given("a request body for the \'Create Maturity Level\' endpoint containing an invalid (.*) parameter")]
        public void GivenARequestBodyForTheCreateMaturityLevelEndpointContainingAnInvalidParameter(string field)
        {
            this.BuildValidRequestBody();

            switch (field)
            {
                case "MaturityCategoryId":
                    this.testHost.RequestBody["MaturityCategoryId"] = "invalid_id";
                    break;

                case "BeltLevel":
                    this.testHost.RequestBody["BeltLevel"] = "DarkDarkBlue";
                    break;

                case "Description":
                    this.testHost.RequestBody["Description"] = 1;
                    break;

                default:
                    break;
            }
        }

        [Given("a request body for the \'Create Maturity Level\' endpoint with a missing (.*) parameter")]
        public void GivenARequestBodyForTheCreateMaturityLevelEndpointWithAMissingParameter(string field)
        {
            this.BuildValidRequestBody();

            this.testHost.RequestBody[field] = null;
        }

        [Then("the Location response header contains the ID of the new resource")]
        public void ThenTheLocationResponseHeaderContainsTheIdOfTheNewResource()
        {
            var locationHeader = this.testHost.LastResponseMessage.Headers.Location;

            this.newResourceId = ulong.Parse
            (
                locationHeader
                    ?.ToString()
                    .Split("/")
                    .LastOrDefault()
            );

            Assert.AreNotEqual(0, this.newResourceId);
        }

        [Then("the MaturityLevel record has been inserted into the database")]
        public async Task ThenTheMaturityLevelRecordHasBeenInsertedIntoTheDatabase()
        {
            var doesRecordExist = await this.maturityLevelDataHelper.DoesRecordExist<MaturityLevelRecord>(this.newResourceId);

            Assert.IsTrue(doesRecordExist);
        }

        private void BuildValidRequestBody()
        {
            this.testHost.RequestBody.Add("MaturityCategoryId", this.maturityCategoryId);
            this.testHost.RequestBody.Add("BeltLevel", BeltType.White.ToString());
            this.testHost.RequestBody.Add("Description", "Must have attended 3 security conferences.");
        }
    }
}
