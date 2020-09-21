using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories
{
    public interface IMaturityLevelsRepository
    {
        Task<Guid> CreateAsync(MaturityLevelRecord maturityLevel);

        Task<MaturityLevelRecord> GetAsync(Guid id);

        Task<IEnumerable<MaturityLevelRecord>> GetAllAsync();

        Task<IEnumerable<MaturityLevelRecord>> GetByCategoryIdAsync(Guid categoryId);

        Task UpdateAsync(MaturityLevelRecord maturityLevel);

        Task DeleteAsync(Guid id);
    }
}
