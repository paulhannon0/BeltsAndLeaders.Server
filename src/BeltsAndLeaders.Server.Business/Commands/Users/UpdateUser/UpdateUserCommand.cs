using BeltsAndLeaders.Server.Business.Models.Users;
using BeltsAndLeaders.Server.Business.Models.Users.UpdateUser;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Models;
using BeltsAndLeaders.Server.Data.Repositories;
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

        public async Task<ulong> ExecuteAsync(UpdateUserCommandRequestModel commandRequest)
        {
            UserRecord existingUser = await this.usersRepository.GetAsync(commandRequest.Id);

            if(existingUser == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"User (ID: {commandRequest.Id}) cannot be found.");
            }

            User user = new User
            {
                Id = commandRequest.Id,
                Name = commandRequest.Name,
                Email = commandRequest.Email,
                SpecialistArea = commandRequest.SpecialistArea
            };

            await this.usersRepository.UpdateAsync(user.ToTableRecord());

            return user.Id;
        }
    }
}
