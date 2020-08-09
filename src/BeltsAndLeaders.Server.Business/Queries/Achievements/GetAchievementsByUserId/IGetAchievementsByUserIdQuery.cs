using System.Collections.Generic;
using BeltsAndLeaders.Server.Business.Models.Achievements;
using BeltsAndLeaders.Server.Business.Models.Achievements.GetAchievementsByUserId;

namespace BeltsAndLeaders.Server.Business.Queries.Achievements.GetAchievementsByUserId
{
    public interface IGetAchievementsByUserIdQuery : IQuery<GetAchievementsByUserIdQueryRequestModel, IEnumerable<Achievement>> { }
}
