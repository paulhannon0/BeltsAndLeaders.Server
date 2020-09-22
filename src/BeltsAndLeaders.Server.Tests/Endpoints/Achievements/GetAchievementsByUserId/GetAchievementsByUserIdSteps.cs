using System;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.Achievements.GetAchievementsByUserId;
using BeltsAndLeaders.Server.Common.Enums;
using BeltsAndLeaders.Server.Tests.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BeltsAndLeaders.Server.Tests.Endpoints.Achievements.GetAchievementsByUserId
{
    [Binding]
    [Scope(Feature = "Get Achievements by User ID")]
    public class GetAchievementsByUserIdSteps
    {
        private readonly TestHost testHost;
        private readonly AchievementDataHelper achievementDataHelper;
        private readonly MaturityCategoryDataHelper maturityCategoryDataHelper;
        private readonly MaturityLevelDataHelper maturityLevelDataHelper;
        private readonly UserDataHelper userDataHelper;
        private Guid validId;
        private Guid maturityCategoryId;
        private Guid maturityLevelId;
        private Guid achievementId;
        private readonly DateTimeOffset achievementDate;
        private readonly string comment;

        public GetAchievementsByUserIdSteps(
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
        [Scope(Feature = "Get Achievements by User ID")]
        public async Task BeforeScenario()
        {
            this.maturityCategoryId = await this.maturityCategoryDataHelper.CreateMaturityCategoryAsync("TestName");
            this.maturityLevelId = await this.maturityLevelDataHelper.CreateMaturityLevelAsync(this.maturityCategoryId, BeltType.White, "Test");
            this.validId = await this.userDataHelper.CreateUserAsync("John Doe", "johndoe@gmail.com", "Security");
            this.achievementId = await this.achievementDataHelper.CreateAchievementAsync
            (
                this.validId,
                this.maturityLevelId,
                this.achievementDate,
                this.comment
            );
        }

        [Given("a valid request path for the \'Get Achievements by User ID\' endpoint")]
        public void GivenAValidRequestPathForTheGetAchievementsByUserIdEndpoint()
        {
            this.SetEndpointPath(this.validId);
        }

        [Given("a request path for the \'Get Achievements by User ID\' endpoint with an invalid UserId parameter")]
        public void GivenARequestPathForTheGetAchievementsByUserIdEndpointWitBeltsAndLeadersInvalidParameter()
        {
            this.SetEndpointPath("invalid_id");
        }

        [Given("a request path for the \'Get Achievements by User ID\' endpoint with an ID for a non-existent resource")]
        public void GivenARequestPathForTheGetAchievementsByUserIdEndpointWitBeltsAndLeadersIdForANonExistentResource()
        {
            this.SetEndpointPath(Guid.NewGuid());
        }

        [Then(@"the Achievement record can be found in the response body")]
        public async Task ThenTheAchievementRecordCanBeFoundInTheResponseBody()
        {
            var body = await this.testHost.ExtractResponseBodyAsync<GetAchievementsByUserIdResponseModel>();

            Assert.AreEqual(1, body.Achievements.Count);

            var achievement = body.Achievements.FirstOrDefault();

            Assert.AreEqual(this.achievementId, achievement.Id);
            Assert.AreEqual(this.validId, achievement.UserId);
            Assert.AreEqual(this.maturityLevelId, achievement.MaturityLevelId);
            Assert.AreEqual(this.achievementDate, achievement.AchievementDate);
            Assert.AreEqual(this.comment, achievement.Comment);
        }

        private void SetEndpointPath(object userId)
        {
            this.testHost.EndpointPath = $"/users/{userId}/achievements";
        }
    }
}
