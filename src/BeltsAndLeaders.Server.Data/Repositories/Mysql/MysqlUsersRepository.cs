using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Helpers;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories.Mysql
{
    public class MysqlUsersRepository : IUsersRepository
    {
        public async Task<ulong> CreateAsync(UserRecord user)
        {
            return await RepositoryHelper.InsertAsync<UserRecord>(user);
        }

        public async Task<UserRecord> GetAsync(ulong id)
        {
            return await RepositoryHelper.GetByIdAsync<UserRecord>(id);
        }

        public async Task<IEnumerable<UserRecord>> GetAllAsync()
        {
            return await RepositoryHelper.GetAllAsync<UserRecord>();
        }

        public async Task UpdateAsync(UserRecord user)
        {
            await RepositoryHelper.UpdateAsync<UserRecord>(user);
        }

        public async Task DeleteAsync(ulong id)
        {
            await RepositoryHelper.DeleteAsync<UserRecord>(new UserRecord { Id = id });
        }
    }
}
