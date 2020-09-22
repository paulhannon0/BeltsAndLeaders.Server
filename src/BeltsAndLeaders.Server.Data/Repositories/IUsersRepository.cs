using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Data.Repositories
{
    public interface IUsersRepository
    {
        Task CreateAsync(UserRecord user);

        Task<UserRecord> GetAsync(Guid id);

        Task<IEnumerable<UserRecord>> GetAllAsync();

        Task UpdateAsync(UserRecord user);

        Task DeleteAsync(Guid id);
    }
}
