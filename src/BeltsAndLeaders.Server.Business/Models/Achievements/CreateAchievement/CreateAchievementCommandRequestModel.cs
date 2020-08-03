using System;

namespace BeltsAndLeaders.Server.Business.Models.Achievements.CreateAchievement
{
    public class CreateAchievementCommandRequestModel
    {
        public ulong UserId { get; set; }

        public ulong MaturityLevelId { get; set; }

        public DateTimeOffset AchievementDate { get; set; }

        public string Comment { get; set; }
    }
}
