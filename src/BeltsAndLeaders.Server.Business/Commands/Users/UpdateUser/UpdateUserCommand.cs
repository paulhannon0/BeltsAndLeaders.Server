using BeltsAndLeaders.Server.Business.Models.Users;
using BeltsAndLeaders.Server.Business.Models.Users.UpdateUser;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Data.Repositories;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BeltsAndLeaders.Server.Business.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : IUpdateUserCommand
    {
        private readonly IUsersRepository usersRepository;

        public UpdateUserCommand(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<Guid> ExecuteAsync(UpdateUserCommandRequestModel commandRequest)
        {
            var userRecord = await this.usersRepository.GetAsync(commandRequest.Id);

            if (userRecord == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"User (ID: {commandRequest.Id}) cannot be found.");
            }

            var existingUser = User.FromTableRecord(userRecord);

            existingUser.Name = commandRequest.Name;
            existingUser.Email = commandRequest.Email;
            existingUser.SpecialistArea = commandRequest.SpecialistArea;

            await this.usersRepository.UpdateAsync(existingUser.ToTableRecord());

            return existingUser.Id;
        }
    }
}
