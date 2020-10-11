using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Users;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.Users.GetAllUsers
{
    public class GetAllUsersQuery : IGetAllUsersQuery
    {
        private readonly IUsersRepository usersRepository;

        public GetAllUsersQuery(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<IEnumerable<User>> ExecuteAsync()
        {
            var users = await this.usersRepository.GetAllAsync();
            var userList = new List<User>();

            foreach (var user in users)
            {
                userList.Add(User.FromTableRecord(user));
            }

            return userList.OrderByDescending(u => u.TotalMaturityPoints);
        }
    }
}
