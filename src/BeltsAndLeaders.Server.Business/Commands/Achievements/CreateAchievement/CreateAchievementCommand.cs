using System;
using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Achievements;
using BeltsAndLeaders.Server.Business.Models.Achievements.CreateAchievement;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Business.Models.Users;
using BeltsAndLeaders.Server.Common.Enums;
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

        public async Task<Guid> ExecuteAsync(CreateAchievementCommandRequestModel commandRequest)
        {
            var userRecord = await this.usersRepository.GetAsync(commandRequest.UserId);

            if (userRecord is null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"User (ID: {commandRequest.UserId}) cannot be found.");
            }

            var maturityLevelRecord = await this.maturityLevelsRepository.GetAsync(commandRequest.MaturityLevelId);

            if (maturityLevelRecord is null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityLevel (ID: {commandRequest.MaturityLevelId}) cannot be found.");
            }

            var user = User.FromTableRecord(userRecord);
            var maturityLevel = MaturityLevel.FromTableRecord(maturityLevelRecord);
            var achievement = new Achievement
            {
                UserId = commandRequest.UserId,
                MaturityLevelId = commandRequest.MaturityLevelId,
                AchievementDate = commandRequest.AchievementDate,
                Comment = commandRequest.Comment
            };

            user.TotalMaturityPoints += (int)maturityLevel.BeltLevel;

            var achievementId = await this.achievementsRepository.CreateAsync(achievement.ToTableRecord());
            var numberOfUniqueAchievements = await this.achievementsRepository.GetUniqueAchievementsCountByUserId(user.Id);
            var numberOfGreenBeltAchievements = await this.achievementsRepository.GetGreenBeltAchievementCountByUserId(user.Id);
            var numberOfBlackBeltAchievements = await this.achievementsRepository.GetBlackBeltAchievementCountByUserId(user.Id);

            if (numberOfUniqueAchievements >= 9 && numberOfBlackBeltAchievements >= 3)
            {
                user.Belt = BeltType.Black;
            }
            else if (numberOfUniqueAchievements >= 3 && numberOfBlackBeltAchievements >= 1 && numberOfGreenBeltAchievements >= 2)
            {
                user.Belt = BeltType.Green;
            }
            else if (numberOfUniqueAchievements >= 5)
            {
                user.Belt = BeltType.White;
            }
            else
            {
                user.Belt = BeltType.None;
            }

            await this.usersRepository.UpdateAsync(user.ToTableRecord());

            return achievementId;
        }
    }
}
