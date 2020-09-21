using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories
{
    public interface IMaturityCategoriesRepository
    {
        Task<Guid> CreateAsync(MaturityCategoryRecord maturityCategory);

        Task<MaturityCategoryRecord> GetAsync(Guid id);

        Task<IEnumerable<MaturityCategoryRecord>> GetAllAsync();

        Task UpdateAsync(MaturityCategoryRecord maturityCategory);

        Task DeleteAsync(Guid id);
    }
}
