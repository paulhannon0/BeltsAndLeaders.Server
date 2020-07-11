using System.Collections.Generic;
using BeltsAndLeaders.Server.Business.Models.Users;

namespace BeltsAndLeaders.Server.Business.Queries.Users.GetAllUsers
{
    public interface IGetAllUsersQuery : IQueryResponseOnly<IEnumerable<User>> { }
}
