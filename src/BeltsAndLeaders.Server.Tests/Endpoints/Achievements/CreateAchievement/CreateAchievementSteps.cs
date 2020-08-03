using System;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Achievements.CreateAchievement
{
    [Binding]
    [Scope(Feature = "Create Achievement")]
    public class CreateAchievementSteps
    {
        private readonly TestHost testHost;
        private readonly AchievementDataHelper achievementDataHelper;
        private readonly UserDataHelper userDataHelper;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private readonly MaturityLevelDataHelper maturityLevelDataHelper;
        private ulong newResourceId;
        private ulong userId;
        private ulong maturityLevelId;

        public CreateAchievementSteps(
            TestHost testHost,
            AchievementDataHelper achievementDataHelper,
            UserDataHelper userDataHelper,
            MaturityCategoryDataHelper maturityCategoryDataHelper,
            MaturityLevelDataHelper maturityLevelDataHelper
        )
        {
            this.testHost = testHost;
            this.achievementDataHelper = achievementDataHelper;
            this.userDataHelper = userDataHelper;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
            this.maturityLevelDataHelper = maturityLevelDataHelper;
        }

        [BeforeScenario]
        [Scope(Feature = "Create Achievement")]
        public async Task BeforeScenario()
        {
            this.userId = await this.userDataHelper.CreateUserAsync("John Doe", "johndoe@hotmail.com", "Security");
            var maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
            this.maturityLevelId = await this.maturityLevelDataHelper.CreateMaturityLevelAsync(maturityCategoryId, 1, "Test");
        }

        [Given("a valid request path for the \'Create Achievement\' endpoint")]
        public void GivenAValidRequestPathForTheCreateAchievementEndpoint()
        {
            this.testHost.EndpointPath = "/achievements";
        }

        [Given("a valid request body for the \'Create Achievement\' endpoint")]
        public void GivenAValidRequestBodyForTheCreateAchievementEndpoint()
        {
            this.BuildValidRequestBody();
        }

        [Given("a request body for the \'Create Achievement\' endpoint containing an invalid (.*) parameter")]
        public void GivenARequestBodyForTheCreateAchievementEndpointContainingAnInvalidParameter(string field)
        {
            this.BuildValidRequestBody();

            switch (field)
            {
                case "UserId":
                    this.testHost.RequestBody["UserId"] = "invalid_id";
                    break;

                case "MaturityLevelId":
                    this.testHost.RequestBody["MaturityLevelId"] = "invalid_id";
                    break;

                case "AchievementDate":
                    this.testHost.RequestBody["AchievementDate"] = "invalid_date";
                    break;

                case "Comment":
                    this.testHost.RequestBody["Comment"] = 1;
                    break;

                default:
                    break;
            }
        }

        [Given("a request body for the \'Create Achievement\' endpoint with a missing (.*) parameter")]
        public void GivenARequestBodyForTheCreateAchievementEndpointWithAMissingParameter(string field)
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

        [Then("the Achievement record has been inserted into the database")]
        public async Task ThenTheAchievementRecordHasBeenInsertedIntoTheDatabase()
        {
            var doesRecordExist = await this.achievementDataHelper.DoesRecordExist<AchievementRecord>(this.newResourceId);

            Assert.IsTrue(doesRecordExist);
        }

        [Then("the relevant User has had their Belt and MaturityLevel values updated in the database")]
        public void ThenTheRelevantUserHasHadTheirBeltAndMaturityLevelValuesUpdatedInTheDatabase()
        {

        }

        private void BuildValidRequestBody()
        {
            this.testHost.RequestBody.Add("UserId", this.userId);
            this.testHost.RequestBody.Add("MaturityLevelId", this.maturityLevelId);
            this.testHost.RequestBody.Add("AchievementDate", DateTimeOffset.Now);
            this.testHost.RequestBody.Add("Comment", "I did good at the thing.");
        }
    }
}
