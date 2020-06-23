using System;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Users;
using BeltsAndLeaders.Server.Business.Models.Users.CreateUser;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.Users.CreateUser
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IUsersRepository usersRepository;

        public CreateUserCommand(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<ulong> ExecuteAsync(CreateUserCommandRequestModel commandRequest)
        {
            var user = new User
            {
                Name = commandRequest.Name,
                Email = commandRequest.Email,
                SpecialistArea = commandRequest.SpecialistArea,
                ChampionStartDate = commandRequest.ChampionStartDate
            };

            return await this.usersRepository.CreateAsync(user.ToTableRecord());
        }
    }
}
