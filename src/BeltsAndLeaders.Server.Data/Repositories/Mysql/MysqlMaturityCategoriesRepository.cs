using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Helpers;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories.Mysql
{
    public class MysqlMaturityCategoriesRepository : IMaturityCategoriesRepository
    {
        public async Task<Guid> CreateAsync(MaturityCategoryRecord maturityCategory)
        {
            return await RepositoryHelper.InsertAsync<MaturityCategoryRecord>(maturityCategory);
        }

        public async Task<MaturityCategoryRecord> GetAsync(Guid id)
        {
            return await RepositoryHelper.GetByIdAsync<MaturityCategoryRecord>(id);
        }

        public async Task<IEnumerable<MaturityCategoryRecord>> GetAllAsync()
        {
            return await RepositoryHelper.GetAllAsync<MaturityCategoryRecord>();
        }

        public async Task UpdateAsync(MaturityCategoryRecord maturityCategory)
        {
            await RepositoryHelper.UpdateAsync<MaturityCategoryRecord>(maturityCategory);
        }

        public async Task DeleteAsync(Guid id)
        {
            await RepositoryHelper.DeleteAsync<MaturityCategoryRecord>(new MaturityCategoryRecord { Id = id });
        }
    }
}
