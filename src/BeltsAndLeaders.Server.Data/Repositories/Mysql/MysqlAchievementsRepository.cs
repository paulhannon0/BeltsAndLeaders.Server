using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Helpers;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories.Mysql
{
    public class MysqlAchievementsRepository : IAchievementsRepository
    {
        public async Task<ulong> CreateAsync(AchievementRecord achievement)
        {
            return await RepositoryHelper.InsertAsync<AchievementRecord>(achievement);
        }

        public async Task<AchievementRecord> GetAsync(ulong id)
        {
            return await RepositoryHelper.GetByIdAsync<AchievementRecord>(id);
        }

        public async Task<IEnumerable<AchievementRecord>> GetAllAsync()
        {
            return await RepositoryHelper.GetAllAsync<AchievementRecord>();
        }

        public async Task<IEnumerable<AchievementRecord>> GetByUserIdAsync(ulong userId)
        {
            return await RepositoryHelper.GetByNonKeyIdValue<AchievementRecord>
            (
                "Achievements",
                "UserId",
                userId
            );
        }

        public async Task UpdateAsync(AchievementRecord achievement)
        {
            await RepositoryHelper.UpdateAsync<AchievementRecord>(achievement);
        }

        public async Task DeleteAsync(ulong id)
        {
            await RepositoryHelper.DeleteAsync<AchievementRecord>(new AchievementRecord { Id = id });
        }
    }
}
