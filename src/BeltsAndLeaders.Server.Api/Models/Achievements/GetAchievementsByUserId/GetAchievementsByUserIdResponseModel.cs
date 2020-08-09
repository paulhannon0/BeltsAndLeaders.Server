using System.Collections.Generic;
using BeltsAndLeaders.Server.Api.Models.Achievements.GetAchievement;
using BeltsAndLeaders.Server.Business.Models.Achievements;

namespace BeltsAndLeaders.Server.Api.Models.Achievements.GetAchievementsByUserId
{
    public class GetAchievementsByUserIdResponseModel
    {
        public List<GetAchievementResponseModel> Achievements { get; set; }

        public static GetAchievementsByUserIdResponseModel FromBusinessModel(IEnumerable<Achievement> achievements)
        {
            var responseAchievements = new List<GetAchievementResponseModel>();

            foreach (var achievement in achievements)
            {
                responseAchievements.Add
                (
                    new GetAchievementResponseModel
                    {
                        Id = achievement.Id,
                        UserId = achievement.UserId,
                        MaturityLevelId = achievement.MaturityLevelId,
                        AchievementDate = achievement.AchievementDate,
                        Comment = achievement.Comment,
                        CreatedAt = achievement.CreatedAt,
                        UpdatedAt = achievement.UpdatedAt
                    }
                );
            }

            return new GetAchievementsByUserIdResponseModel
            {
                Achievements = responseAchievements
            };
        }
    }
}
