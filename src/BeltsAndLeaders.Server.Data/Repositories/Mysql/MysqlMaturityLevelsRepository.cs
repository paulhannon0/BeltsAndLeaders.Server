using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Helpers;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories.Mysql
{
    public class MysqlMaturityLevelsRepository : IMaturityLevelsRepository
    {
        public async Task CreateAsync(MaturityLevelRecord maturityLevel)
        {
            await RepositoryHelper.InsertAsync<MaturityLevelRecord>(maturityLevel);
        }

        public async Task<MaturityLevelRecord> GetAsync(Guid id)
        {
            return await RepositoryHelper.GetByIdAsync<MaturityLevelRecord>(id.ToByteArray());
        }

        public async Task<IEnumerable<MaturityLevelRecord>> GetAllAsync()
        {
            return await RepositoryHelper.GetAllAsync<MaturityLevelRecord>();
        }

        public async Task<IEnumerable<MaturityLevelRecord>> GetByCategoryIdAsync(Guid categoryId)
        {
            return await RepositoryHelper.GetByNonKeyIdValue<MaturityLevelRecord>
            (
                "MaturityLevels",
                "MaturityCategoryId",
                categoryId.ToByteArray()
            );
        }

        public async Task UpdateAsync(MaturityLevelRecord maturityLevel)
        {
            await RepositoryHelper.UpdateAsync<MaturityLevelRecord>(maturityLevel);
        }

        public async Task DeleteAsync(Guid id)
        {
            await RepositoryHelper.DeleteAsync<MaturityLevelRecord>(new MaturityLevelRecord { Id = id.ToByteArray() });
        }
    }
}
