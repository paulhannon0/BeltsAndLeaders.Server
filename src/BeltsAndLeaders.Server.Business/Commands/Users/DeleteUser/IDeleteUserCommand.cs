using BeltsAndLeaders.Server.Business.Models.Users.DeleteUser;

namespace BeltsAndLeaders.Server.Business.Commands.Users.DeleteUser
{
    public interface IDeleteUserCommand : ICommand<DeleteUserCommandRequestModel, ulong>
    {
    }
}
