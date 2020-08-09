using System;
using BeltsAndLeaders.Server.Business.Models.Achievements;

namespace BeltsAndLeaders.Server.Api.Models.Achievements.GetAchievement
{
    public class GetAchievementResponseModel
    {
        public ulong Id { get; set; }

        public ulong UserId { get; set; }

        public ulong MaturityLevelId { get; set; }

        public DateTimeOffset AchievementDate { get; set; }

        public string Comment { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static GetAchievementResponseModel FromBusinessModel(Achievement achievement)
        {
            return new GetAchievementResponseModel
            {
                Id = achievement.Id,
                UserId = achievement.UserId,
                MaturityLevelId = achievement.MaturityLevelId,
                AchievementDate = achievement.AchievementDate,
                Comment = achievement.Comment,
                CreatedAt = achievement.CreatedAt,
                UpdatedAt = achievement.UpdatedAt
            };
        }
    }
}
