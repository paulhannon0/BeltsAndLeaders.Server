using BeltsAndLeaders.Server.Business.Models.Users;
using BeltsAndLeaders.Server.Business.Models.Users.GetUser;

namespace BeltsAndLeaders.Server.Business.Queries.Users.GetUser
{
    public interface IGetUserQuery : IQuery<GetUserQueryRequestModel, User> { }
}
