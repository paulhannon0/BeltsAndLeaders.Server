using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Common.Steps
{
    [Binding]
    public class ResponseSteps
    {
        private readonly TestHost testHost;

        public ResponseSteps(TestHost testHost)
        {
            this.testHost = testHost;
        }

        [Then(@"\((.*)\) (.*) is returned")]
        public void ThenStatusCodeIsReturned(int expectedStatusCode, string status)
        {
            var actualStatusCode = (int)this.testHost.LastResponseMessage.StatusCode;

            Assert.AreEqual(expectedStatusCode, actualStatusCode);
        }
    }
}
