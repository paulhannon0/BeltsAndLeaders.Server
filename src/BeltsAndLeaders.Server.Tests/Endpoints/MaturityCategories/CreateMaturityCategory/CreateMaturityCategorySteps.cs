using System;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.MaturityCategories.CreateMaturityCategory
{
    [Binding]
    [Scope(Feature = "Create Maturity Category")]
    public class CreateMaturityCategorySteps
    {
        private readonly TestHost testHost;
        private readonly WidgetDataHelper widgetDataHelper;
        private ulong newResourceId;

        public CreateMaturityCategorySteps(TestHost testHost, WidgetDataHelper widgetDataHelper)
        {
            this.testHost = testHost;
            this.widgetDataHelper = widgetDataHelper;
        }

        [Given("a valid request path for the \'Create Maturity Category\' endpoint")]
        public void GivenAValidRequestPathForTheCreateMaturityCategoryEndpoint()
        {
            this.testHost.EndpointPath = "/maturity-categories";
        }

        [Given("a valid request body for the \'Create Maturity Category\' endpoint")]
        public void GivenAValidRequestBodyForTheCreateMaturityCategoryEndpoint()
        {
            this.BuildValidRequestBody();
        }

        [Given("a request body for the \'Create Maturity Category\' endpoint containing an invalid (.*) parameter")]
        public void GivenARequestBodyForTheCreateMaturityCategoryEndpointContainingAnInvalidParameter(string field)
        {
            this.BuildValidRequestBody();

            switch (field)
            {
                case "Name":
                    this.testHost.RequestBody["Name"] = 1;
                    break;

                default:
                    break;
            }
        }

        [Given("a request body for the \'Create Maturity Category\' endpoint with a missing (.*) parameter")]
        public void GivenARequestBodyForTheCreateMaturityCategoryEndpointWithAMissingParameter(string field)
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

        [Then("the MaturityCategory record has been inserted into the database")]
        public async Task ThenTheMaturityCategoryRecordHasBeenInsertedIntoTheDatabase()
        {
            var doesRecordExist = await this.widgetDataHelper.DoesRecordExist<MaturityCategoryRecord>(this.newResourceId);

            Assert.IsTrue(doesRecordExist);
        }

        private void BuildValidRequestBody()
        {
            this.testHost.RequestBody.Add("Name", Guid.NewGuid().ToString());
        }
    }
}
