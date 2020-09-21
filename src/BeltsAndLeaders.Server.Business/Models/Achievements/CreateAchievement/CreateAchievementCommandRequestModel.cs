using System;

namespace BeltsAndLeaders.Server.Business.Models.Achievements.CreateAchievement
{
    public class CreateAchievementCommandRequestModel
    {
        public Guid UserId { get; set; }

        public Guid MaturityLevelId { get; set; }

        public DateTimeOffset AchievementDate { get; set; }

        public string Comment { get; set; }
    }
}
