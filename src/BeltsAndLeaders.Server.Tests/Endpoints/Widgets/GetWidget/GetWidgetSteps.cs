using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Widgets.GetWidget;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Widgets.GetWidget
{
    [Binding]
    [Scope(Feature = "Get Widget")]
    public class GetWidgetSteps
    {
        private readonly TestHost testHost;
        private readonly WidgetDataHelper widgetDataHelper;
        private ulong validId;
        private readonly string invalidId;
        private readonly ulong nonExistentId;
        private readonly string widgetName;

        public GetWidgetSteps(TestHost testHost, WidgetDataHelper widgetDataHelper)
        {
            this.testHost = testHost;
            this.widgetDataHelper = widgetDataHelper;
            this.invalidId = "invalid_id";
            this.nonExistentId = 0;
            this.widgetName = "WidgetName";
        }

        [BeforeScenario]
        [Scope(Feature = "Get Widget")]
        public async Task BeforeScenario()
        {
            this.validId = await this.widgetDataHelper.CreateWidgetAsync(this.widgetName);
        }

        [Given("a valid request path for the \'Get Widget\' endpoint")]
        public void GivenAValidRequestPathForTheGetWidgetEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Get Widget\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheGetWidgetEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath(this.invalidId);
        }

        [Given("a request path for the \'Get Widget\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheGetWidgetEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(this.nonExistentId);
        }

        [Then(@"the Widget record can be found in the response body")]
        public async Task ThenTheWidgetRecordCanBeFoundInTheResponseBody()
        {
            var widget = await this.testHost.ExtractResponseBodyAsync<GetWidgetResponseModel>();

            Assert.IsTrue(widget.Id > 0);
            Assert.AreEqual(this.widgetName, widget.Name);
        }

        private void SetEndpointPath(object widgetId)
        {
            this.testHost.EndpointPath = $"/widgets/{widgetId}";
        }
    }
}
