using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories
{
    public interface IAchievementsRepository
    {
        Task<ulong> CreateAsync(AchievementRecord achievement);

        Task<AchievementRecord> GetAsync(ulong id);

        Task<IEnumerable<AchievementRecord>> GetAllAsync();

        Task<IEnumerable<AchievementRecord>> GetByUserIdAsync(ulong userId);

        Task UpdateAsync(AchievementRecord achievement);

        Task DeleteAsync(ulong id);
    }
}
