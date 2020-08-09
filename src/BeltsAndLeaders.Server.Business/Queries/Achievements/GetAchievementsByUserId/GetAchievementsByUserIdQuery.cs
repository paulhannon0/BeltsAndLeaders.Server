using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Achievements;
using BeltsAndLeaders.Server.Business.Models.Achievements.GetAchievementsByUserId;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.Achievements.GetAchievementsByUserId
{
    public class GetAchievementsByUserIdQuery : IGetAchievementsByUserIdQuery
    {
        private readonly IUsersRepository usersRepository;
        private readonly IAchievementsRepository achievementsRepository;

        public GetAchievementsByUserIdQuery(
            IUsersRepository usersRepository,
            IAchievementsRepository achievementsRepository
        )
        {
            this.usersRepository = usersRepository;
            this.achievementsRepository = achievementsRepository;
        }

        public async Task<IEnumerable<Achievement>> ExecuteAsync(GetAchievementsByUserIdQueryRequestModel queryRequest)
        {
            var user = await this.usersRepository.GetAsync(queryRequest.UserId);

            if (user is null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"User (ID: {queryRequest.UserId}) cannot be found.");
            }

            var achievementRecords = await this.achievementsRepository.GetByUserIdAsync(queryRequest.UserId);
            var achievements = new List<Achievement>();

            foreach (var achievementRecord in achievementRecords)
            {
                achievements.Add(Achievement.FromTableRecord(achievementRecord));
            }

            return achievements;
        }
    }
}
