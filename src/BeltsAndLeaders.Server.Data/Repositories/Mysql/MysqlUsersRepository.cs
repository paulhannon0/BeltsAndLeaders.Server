using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories.Mysql
{
    public class MysqlUsersRepository : IUsersRepository
    {
        public async Task<ulong> CreateAsync(UserRecord widget)
        {
            return await RepositoryHelper.InsertAsync<UserRecord>(widget);
        }

        public async Task<UserRecord> GetAsync(ulong id)
        {
            return await RepositoryHelper.GetByIdAsync<UserRecord>(id);
        }

        public async Task UpdateAsync(UserRecord widget)
        {
            await RepositoryHelper.UpdateAsync<UserRecord>(widget);
        }

        public async Task DeleteAsync(ulong id)
        {
            await RepositoryHelper.DeleteAsync<UserRecord>(new UserRecord { Id = id });
        }
    }
}
