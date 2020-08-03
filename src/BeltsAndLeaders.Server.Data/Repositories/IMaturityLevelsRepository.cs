using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories
{
    public interface IMaturityLevelsRepository
    {
        Task<ulong> CreateAsync(MaturityLevelRecord maturityLevel);

        Task<MaturityLevelRecord> GetAsync(ulong id);

        Task<IEnumerable<MaturityLevelRecord>> GetAllAsync();

        Task<IEnumerable<MaturityLevelRecord>> GetByCategoryIdAsync(ulong categoryId);

        Task UpdateAsync(MaturityLevelRecord maturityLevel);

        Task DeleteAsync(ulong id);
    }
}
