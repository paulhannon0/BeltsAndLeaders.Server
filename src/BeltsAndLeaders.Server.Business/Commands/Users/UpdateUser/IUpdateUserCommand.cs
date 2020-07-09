using BeltsAndLeaders.Server.Business.Models.Users.UpdateUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeltsAndLeaders.Server.Business.Commands.Users.UpdateUser
{
    public interface IUpdateUserCommand
    {
        public interface IUpdateUserCommand : ICommand<UpdateUserCommandRequestModel, ulong> { }
    }
}
