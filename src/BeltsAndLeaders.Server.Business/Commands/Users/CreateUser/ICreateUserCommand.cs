using System;
using BeltsAndLeaders.Server.Business.Models.Users.CreateUser;

namespace BeltsAndLeaders.Server.Business.Commands.Users.CreateUser
{
    public interface ICreateUserCommand : ICommand<CreateUserCommandRequestModel, Guid> { }
}
