using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories
{
    public interface IAchievementsRepository
    {
        Task CreateAsync(AchievementRecord achievement);

        Task<AchievementRecord> GetAsync(Guid id);

        Task<IEnumerable<AchievementRecord>> GetAllAsync();

        Task<IEnumerable<AchievementRecord>> GetByUserIdAsync(Guid userId);

        Task<int> GetGreenBeltAchievementCountByUserId(Guid userId);

        Task<int> GetBlackBeltAchievementCountByUserId(Guid userId);

        Task<int> GetUniqueAchievementsCountByUserId(Guid userId);

        Task UpdateAsync(AchievementRecord achievement);

        Task DeleteAsync(Guid id);
    }
}
