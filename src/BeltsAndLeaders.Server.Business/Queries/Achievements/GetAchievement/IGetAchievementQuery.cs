using BeltsAndLeaders.Server.Business.Models.Achievements;
using BeltsAndLeaders.Server.Business.Models.Achievements.GetAchievement;

namespace BeltsAndLeaders.Server.Business.Queries.Achievements.GetAchievement
{
    public interface IGetAchievementQuery : IQuery<GetAchievementQueryRequestModel, Achievement> { }
}
