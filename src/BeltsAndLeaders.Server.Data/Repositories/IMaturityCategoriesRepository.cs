using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories
{
    public interface IMaturityCategoriesRepository
    {
        Task<ulong> CreateAsync(MaturityCategoryRecord widget);

        Task<MaturityCategoryRecord> GetAsync(ulong id);

        Task<IEnumerable<MaturityCategoryRecord>> GetAllAsync();

        Task UpdateAsync(MaturityCategoryRecord widget);

        Task DeleteAsync(ulong id);
    }
}
