using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Helpers;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories.Mysql
{
    public class MysqlAchievementsRepository : IAchievementsRepository
    {
        public async Task<Guid> CreateAsync(AchievementRecord achievement)
        {
            return await RepositoryHelper.InsertAsync<AchievementRecord>(achievement);
        }

        public async Task<AchievementRecord> GetAsync(Guid id)
        {
            return await RepositoryHelper.GetByIdAsync<AchievementRecord>(id);
        }

        public async Task<IEnumerable<AchievementRecord>> GetAllAsync()
        {
            return await RepositoryHelper.GetAllAsync<AchievementRecord>();
        }

        public async Task<IEnumerable<AchievementRecord>> GetByUserIdAsync(Guid userId)
        {
            return await RepositoryHelper.GetByNonKeyIdValue<AchievementRecord>
            (
                "Achievements",
                "UserId",
                userId
            );
        }

        public async Task<int> GetGreenBeltAchievementCountByUserId(Guid userId)
        {
            return await AchievementsRepositoryHelper.GetAchievementCountByUserIdAndBeltColour(userId, "Green");
        }

        public async Task<int> GetBlackBeltAchievementCountByUserId(Guid userId)
        {
            return await AchievementsRepositoryHelper.GetAchievementCountByUserIdAndBeltColour(userId, "Black");
        }

        public async Task<int> GetUniqueAchievementsCountByUserId(Guid userId)
        {
            return await AchievementsRepositoryHelper.GetUniqueAchievementsCountByUserId(userId);
        }

        public async Task UpdateAsync(AchievementRecord achievement)
        {
            await RepositoryHelper.UpdateAsync<AchievementRecord>(achievement);
        }

        public async Task DeleteAsync(Guid id)
        {
            await RepositoryHelper.DeleteAsync<AchievementRecord>(new AchievementRecord { Id = id });
        }
    }
}
