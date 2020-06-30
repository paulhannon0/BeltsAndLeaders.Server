using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Widgets.DeleteWidget
{
    [Binding]
    [Scope(Feature = "Delete Widget")]
    public class DeleteWidgetSteps
    {
        private readonly TestHost testHost;
        private readonly WidgetDataHelper widgetDataHelper;
        private ulong validId;
        private readonly string invalidId;
        private readonly ulong nonExistentId;

        public DeleteWidgetSteps(TestHost testHost, WidgetDataHelper widgetDataHelper)
        {
            this.testHost = testHost;
            this.widgetDataHelper = widgetDataHelper;
            this.invalidId = "invalid_id";
            this.nonExistentId = 0;
        }

        [BeforeScenario]
        [Scope(Feature = "Delete Widget")]
        public async Task BeforeScenario()
        {
            this.validId = await this.widgetDataHelper.CreateWidgetAsync("WidgetName");
        }

        [Given("a valid request path for the \'Delete Widget\' endpoint")]
        public void GivenAValidRequestPathForTheDeleteWidgetEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Delete Widget\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheDeleteWidgetEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath(this.invalidId);
        }

        [Given("a request path for the \'Delete Widget\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheDeleteWidgetEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(this.nonExistentId);
        }

        [Then(@"the Widget record has been deleted from the database")]
        public async Task ThenTheWidgetRecordHasBeenDeletedFromTheDatabase()
        {
            var doesRecordExist = await this.widgetDataHelper.DoesRecordExist<WidgetRecord>(this.validId);

            Assert.IsFalse(doesRecordExist);
        }

        private void SetEndpointPath(object widgetId)
        {
            this.testHost.EndpointPath = $"/widgets/{widgetId}";
        }
    }
}
