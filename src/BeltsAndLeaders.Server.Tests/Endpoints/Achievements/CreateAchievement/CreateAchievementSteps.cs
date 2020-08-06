using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Common.Enums;
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
        private ulong maturityCategoryId;
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
            this.maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
        }

        [Given("a user with (.*) total maturity points")]
        public async Task GivenAUserWithTotalMaturityPoints(int totalMaturityPoints)
        {
            var whiteBeltLevelIds = await this.CreateWhiteBeltMaturityLevels(totalMaturityPoints);

            await this.CreateAchievements(whiteBeltLevelIds);
        }

        [Given("a user with 9 total maturity points including 2 green belt and 1 black belt achievements")]
        public async Task GivenAUserWith9TotalMaturityPointsIncluding2GreenBeltAnd1BlackBeltAchievements()
        {
            var whiteBeltLevelIds = await this.CreateWhiteBeltMaturityLevels(2);
            var greenBeltLevelIds = await this.CreateGreenBeltMaturityLevels(2);
            var blackBeltLevelIds = await this.CreateBlackBeltMaturityLevels(1);

            await this.CreateAchievements(whiteBeltLevelIds);
            await this.CreateAchievements(greenBeltLevelIds);
            await this.CreateAchievements(blackBeltLevelIds);
        }

        [Given("a user with 14 total maturity points including 3 black belt achievements")]
        public async Task GivenAUserWith14TotalMaturityPointsIncluding3BlackBeltAchievements()
        {
            var whiteBeltLevelIds = await this.CreateWhiteBeltMaturityLevels(5);
            var blackBeltLevelIds = await this.CreateBlackBeltMaturityLevels(3);

            await this.CreateAchievements(whiteBeltLevelIds);
            await this.CreateAchievements(blackBeltLevelIds);
        }

        [Given("a valid request path for the \'Create Achievement\' endpoint")]
        public void GivenAValidRequestPathForTheCreateAchievementEndpoint()
        {
            this.testHost.EndpointPath = "/achievements";
        }

        [Given("a valid request body for the \'Create Achievement\' endpoint with a (White|Green|Black) belt achievement")]
        public async Task GivenAValidRequestBodyForTheCreateAchievementEndpoint(string belt)
        {
            this.maturityLevelId = await this.maturityLevelDataHelper.CreateMaturityLevelAsync
            (
                maturityCategoryId,
                (BeltType)Enum.Parse(typeof(BeltType), belt),
                "Test"
            );

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

        [Then("the relevant User has had their Belt value updated to (None|White|Green|Black)")]
        public async Task ThenTheRelevantUserHasHadTheirBeltValueUpdatedTo(string belt)
        {
            var user = await this.userDataHelper.GetUserAsync(this.userId);

            Assert.AreEqual(belt, user.Belt.ToString());
        }

        [Then("the relevant User has had their TotalMaturityPoints value updated to (.*)")]
        public async Task ThenTheRelevantUserHasHadTheirTotalMaturityPointsValueUpdatedTo(int totalMaturityPoints)
        {
            var user = await this.userDataHelper.GetUserAsync(this.userId);

            Assert.AreEqual(totalMaturityPoints, user.TotalMaturityPoints);
        }

        private void BuildValidRequestBody()
        {
            this.testHost.RequestBody.Add("UserId", this.userId);
            this.testHost.RequestBody.Add("MaturityLevelId", this.maturityLevelId);
            this.testHost.RequestBody.Add("AchievementDate", DateTimeOffset.Now);
            this.testHost.RequestBody.Add("Comment", "I did good at the thing.");
        }

        private async Task<IEnumerable<ulong>> CreateWhiteBeltMaturityLevels(int count)
        {
            return await this.CreateMaturityLevels(count, BeltType.White);
        }

        private async Task<IEnumerable<ulong>> CreateGreenBeltMaturityLevels(int count)
        {
            return await this.CreateMaturityLevels(count, BeltType.Green);
        }

        private async Task<IEnumerable<ulong>> CreateBlackBeltMaturityLevels(int count)
        {
            return await this.CreateMaturityLevels(count, BeltType.Black);
        }

        private async Task<IEnumerable<ulong>> CreateMaturityLevels(int count, BeltType beltType)
        {
            var recordIds = new List<ulong>();

            for (int i = 0; i < count; i++)
            {
                recordIds.Add
                (
                    await this.maturityLevelDataHelper.CreateMaturityLevelAsync
                    (
                        this.maturityCategoryId,
                        beltType,
                        "Test"
                    )
                );
            }

            return recordIds;
        }

        private async Task CreateAchievements(IEnumerable<ulong> maturityLevelIds)
        {
            foreach (var maturityLevelId in maturityLevelIds)
            {
                await this.achievementDataHelper.CreateAchievementAsync
                (
                    this.userId,
                    maturityLevelId,
                    DateTimeOffset.Now,
                    "Test"
                );
            }
        }
    }
}
