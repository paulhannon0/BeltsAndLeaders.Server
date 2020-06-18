using System;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Widgets.CreateWidget
{
    [Binding]
    [Scope(Feature = "Create Widget")]
    public class CreateWidgetSteps
    {
        private readonly TestHost testHost;
        private readonly TestDataHelper testDataHelper;
        private readonly string validName;
        private readonly int invalidName;
        private ulong newResourceId;

        public CreateWidgetSteps(TestHost testHost, TestDataHelper testDataHelper)
        {
            this.testHost = testHost;
            this.testDataHelper = testDataHelper;
            this.validName = Guid.NewGuid().ToString();
            this.invalidName = 1;
        }

        [Given("a valid request path for the \'Create Widget\' endpoint")]
        public void GivenAValidRequestPathForTheCreateWidgetEndpoint()
        {
            this.testHost.EndpointPath = "/widgets";
        }

        [Given("a valid request body for the \'Create Widget\' endpoint")]
        public void GivenAValidRequestBodyForTheCreateWidgetEndpoint()
        {
            this.testHost.RequestBody.Add("Name", this.validName);
        }

        [Given("a request body for the \'Create Widget\' endpoint containing an invalid (.*) parameter")]
        public void GivenARequestBodyForTheCreateWidgetEndpointContainingAnInvalidParameter(string field)
        {
            switch (field)
            {
                case "Name":
                    this.testHost.RequestBody.Add("Name", this.invalidName);
                    break;

                default:
                    break;
            }
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

        [Then("the Widget record has been inserted into the database")]
        public async Task ThenTheWidgetRecordHasBeenInsertedIntoTheDatabase()
        {
            var doesRecordExist = await this.testDataHelper.DoesRecordExist<WidgetRecord>(this.newResourceId);

            Assert.IsTrue(doesRecordExist);
        }
    }
}
