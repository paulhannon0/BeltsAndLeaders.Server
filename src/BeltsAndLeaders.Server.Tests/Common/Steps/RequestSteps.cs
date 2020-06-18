using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Common.Steps
{
    [Binding]
    public class RequestSteps
    {
        private readonly TestHost testHost;

        public RequestSteps(TestHost testHost)
        {
            this.testHost = testHost;
        }

        [When("the (.*) request is made")]
        public async Task WhenTheRequestIsMade(string method)
        {
            switch (method)
            {
                case "GET":
                    await this.testHost.GetAsync();
                    break;

                case "POST":
                    await this.testHost.PostAsync();
                    break;

                case "PUT":
                    await this.testHost.PutAsync();
                    break;

                case "DELETE":
                    await this.testHost.DeleteAsync();
                    break;

                default:
                    throw new ArgumentException($"{method} is not a valid HTTP method.");
            }
        }
    }
}
