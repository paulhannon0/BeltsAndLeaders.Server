using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Achievements;
using BeltsAndLeaders.Server.Business.Models.Achievements.GetAchievement;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.Achievements.GetAchievement
{
    public class GetAchievementQuery : IGetAchievementQuery
    {
        private readonly IAchievementsRepository achievementsRepository;

        public GetAchievementQuery(IAchievementsRepository achievementsRepository)
        {
            this.achievementsRepository = achievementsRepository;
        }

        public async Task<Achievement> ExecuteAsync(GetAchievementQueryRequestModel queryRequest)
        {
            var achievement = await this.achievementsRepository.GetAsync(queryRequest.Id);

            if (achievement == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"Achievement (ID: {queryRequest.Id}) cannot be found.");
            }

            return Achievement.FromTableRecord(achievement);
        }
    }
}
