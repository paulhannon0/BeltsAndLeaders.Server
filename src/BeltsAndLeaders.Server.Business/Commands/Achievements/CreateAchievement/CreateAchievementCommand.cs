using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Achievements;
using BeltsAndLeaders.Server.Business.Models.Achievements.CreateAchievement;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.Achievements.CreateAchievement
{
    public class CreateAchievementCommand : ICreateAchievementCommand
    {
        private readonly IAchievementsRepository achievementsRepository;
        private readonly IMaturityLevelsRepository maturityLevelsRepository;
        private readonly IUsersRepository usersRepository;

        public CreateAchievementCommand(
            IAchievementsRepository achievementsRepository,
            IMaturityLevelsRepository maturityLevelsRepository,
            IUsersRepository usersRepository
        )
        {
            this.achievementsRepository = achievementsRepository;
            this.maturityLevelsRepository = maturityLevelsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<ulong> ExecuteAsync(CreateAchievementCommandRequestModel commandRequest)
        {
            var user = await this.usersRepository.GetAsync(commandRequest.UserId);

            if (user is null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"User (ID: {commandRequest.UserId}) cannot be found.");
            }

            var maturityLevel = await this.maturityLevelsRepository.GetAsync(commandRequest.MaturityLevelId);

            if (maturityLevel is null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityLevel (ID: {commandRequest.MaturityLevelId}) cannot be found.");
            }

            var achievement = new Achievement
            {
                UserId = commandRequest.UserId,
                MaturityLevelId = commandRequest.MaturityLevelId,
                AchievementDate = commandRequest.AchievementDate,
                Comment = commandRequest.Comment
            };

            // TODO: Belt logic

            return await this.achievementsRepository.CreateAsync(achievement.ToTableRecord());
        }
    }
}
