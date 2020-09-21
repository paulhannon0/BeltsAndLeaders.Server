using System;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Achievements.GetAchievement;
using BeltsAndLeaders.Server.Common.Enums;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Achievements.GetAchievement
{
    [Binding]
    [Scope(Feature = "Get Achievement")]
    public class GetAchievementSteps
    {
        private readonly TestHost testHost;
        private readonly AchievementDataHelper achievementDataHelper;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private readonly MaturityLevelDataHelper maturityLevelDataHelper;
        private readonly UserDataHelper userDataHelper;
        private Guid validId;
        private Guid maturityCategoryId;
        private Guid maturityLevelId;
        private Guid userId;
        private readonly DateTimeOffset achievementDate;
        private readonly string comment;

        public GetAchievementSteps(
            TestHost testHost,
            AchievementDataHelper achievementDataHelper,
            MaturityCategoryDataHelper maturityCategoryDataHelper,
            MaturityLevelDataHelper maturityLevelDataHelper,
            UserDataHelper userDataHelper
        )
        {
            this.testHost = testHost;
            this.achievementDataHelper = achievementDataHelper;
            this.maturityCategoryDataHelper = maturityCategoryDataHelper;
            this.maturityLevelDataHelper = maturityLevelDataHelper;
            this.userDataHelper = userDataHelper;
            /* 
                Conversion from/to Unix timestamp is necessary here to mimic the
                same operation the server does when inserting into the DB, resulting
                in a DateTimeOffset to 3 decimal places after the seconds rather than 6,
                so 2020-08-09 16:05:15.7954576+00:00 becomes 2020-08-09 16:05:15.795+00:00
            */
            this.achievementDate = DateTimeOffset.FromUnixTimeMilliseconds
            (
                DateTimeOffset.Now
                    .Subtract(TimeSpan.FromDays(7))
                    .ToUniversalTime()
                    .ToUnixTimeMilliseconds()
            );
            this.comment = "I did X, Y and Z to achieve this maturity level.";
        }

        [BeforeScenario]
        [Scope(Feature = "Get Achievement")]
        public async Task BeforeScenario()
        {
            this.maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
            this.maturityLevelId = await this.maturityLevelDataHelper.CreateMaturityLevelAsync(this.maturityCategoryId, BeltType.White, "Test");
            this.userId = await this.userDataHelper.CreateUserAsync("John Doe", "johndoe@gmail.com", "Security");
            this.validId = await this.achievementDataHelper.CreateAchievementAsync
            (
                this.userId,
                this.maturityLevelId,
                this.achievementDate,
                this.comment
            );
        }

        [Given("a valid request path for the \'Get Achievement\' endpoint")]
        public void GivenAValidRequestPathForTheGetAchievementEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Get Achievement\' endpoint with an invalid (.*) parameter")]
        public void GivenARequestPathForTheGetAchievementEndpointWitBeltsAndLeadersInvalidParameter(string field)
        {
            this.SetEndpointPath("invalid_id");
        }

        [Given("a request path for the \'Get Achievement\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheGetAchievementEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(0);
        }

        [Then(@"the Achievement record can be found in the response body")]
        public async Task ThenTheAchievementRecordCanBeFoundInTheResponseBody()
        {
            var achievement = await this.testHost.ExtractResponseBodyAsync<GetAchievementResponseModel>();

            // Assert.IsTrue(achievement.Id > 0);
            Assert.AreEqual(this.userId, achievement.UserId);
            Assert.AreEqual(this.maturityLevelId, achievement.MaturityLevelId);
            Assert.AreEqual(this.achievementDate, achievement.AchievementDate);
            Assert.AreEqual(this.comment, achievement.Comment);
        }

        private void SetEndpointPath(object achievementId)
        {
            this.testHost.EndpointPath = $"/achievements/{achievementId}";
        }
    }
}
