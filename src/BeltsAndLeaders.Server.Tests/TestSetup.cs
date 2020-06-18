using NUnit.Framework;
using TechTalk.SpecFlow;

[assembly: NonParallelizable]

namespace BeltsAndLeaders.Server.Tests
{
    [Binding]
    public class TestSetup
    {
        [BeforeTestRun]
        public static void SetUp()
        {
            TestConfiguration.Apply();
        }
    }
}
