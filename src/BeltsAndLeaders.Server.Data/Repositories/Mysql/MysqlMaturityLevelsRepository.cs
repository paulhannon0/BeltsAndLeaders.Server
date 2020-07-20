using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Helpers;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories.Mysql
{
    public class MysqlMaturityLevelsRepository : IMaturityLevelsRepository
    {
        public async Task<ulong> CreateAsync(MaturityLevelRecord widget)
        {
            return await RepositoryHelper.InsertAsync<MaturityLevelRecord>(widget);
        }

        public async Task<MaturityLevelRecord> GetAsync(ulong id)
        {
            return await RepositoryHelper.GetByIdAsync<MaturityLevelRecord>(id);
        }

        public async Task<IEnumerable<MaturityLevelRecord>> GetAllAsync()
        {
            return await RepositoryHelper.GetAllAsync<MaturityLevelRecord>();
        }

        public async Task UpdateAsync(MaturityLevelRecord widget)
        {
            await RepositoryHelper.UpdateAsync<MaturityLevelRecord>(widget);
        }

        public async Task DeleteAsync(ulong id)
        {
            await RepositoryHelper.DeleteAsync<MaturityLevelRecord>(new MaturityLevelRecord { Id = id });
        }
    }
}
