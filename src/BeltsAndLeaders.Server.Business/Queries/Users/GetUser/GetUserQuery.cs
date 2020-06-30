using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Users;
using BeltsAndLeaders.Server.Business.Models.Users.GetUser;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.Users.GetUser
{
    public class GetUserQuery : IGetUserQuery
    {
        private readonly IUsersRepository usersRepository;

        public GetUserQuery(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<User> ExecuteAsync(GetUserQueryRequestModel queryRequest)
        {
            var user = await this.usersRepository.GetAsync(queryRequest.Id);

            if (user == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"User (ID: {queryRequest.Id}) cannot be found.");
            }

            return User.FromTableRecord(user);
        }
    }
}
