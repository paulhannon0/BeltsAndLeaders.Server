using BeltsAndLeaders.Server.Business.Models.Users.UpdateUser;

namespace BeltsAndLeaders.Server.Business.Commands.Users.UpdateUser
{
    public interface IUpdateUserCommand : ICommand<UpdateUserCommandRequestModel, ulong> { }
}
