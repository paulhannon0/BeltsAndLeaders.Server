using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories
{
    public interface IMaturityLevelsRepository
    {
        Task<ulong> CreateAsync(MaturityLevelRecord widget);

        Task<MaturityLevelRecord> GetAsync(ulong id);

        Task<IEnumerable<MaturityLevelRecord>> GetAllAsync();

        Task UpdateAsync(MaturityLevelRecord widget);

        Task DeleteAsync(ulong id);
    }
}
