using BeltsAndLeaders.Server.Business.Models.Users.DeleteUser;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BeltsAndLeaders.Server.Business.Commands.Users.DeleteUser
{
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly IUsersRepository usersRepository;

        public DeleteUserCommand(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<Guid> ExecuteAsync(DeleteUserCommandRequestModel commandRequest)
        {
            var user = await this.usersRepository.GetAsync(commandRequest.Id);

            if (user == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"User (ID: {commandRequest.Id}) cannot be found.");
            }

            await this.usersRepository.DeleteAsync(commandRequest.Id);

            return commandRequest.Id;
        }
    }
}
