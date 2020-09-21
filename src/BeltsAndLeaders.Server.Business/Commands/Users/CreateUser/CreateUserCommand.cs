using System;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.Users;
using BeltsAndLeaders.Server.Business.Models.Users.CreateUser;
using BeltsAndLeaders.Server.Common.Enums;
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

        public async Task<Guid> ExecuteAsync(CreateUserCommandRequestModel commandRequest)
        {
            var user = new User
            {
                Name = commandRequest.Name,
                Email = commandRequest.Email,
                TotalMaturityPoints = 0,
                Belt = BeltType.None,
                SpecialistArea = commandRequest.SpecialistArea,
                ChampionStartDate = commandRequest.ChampionStartDate
            };

            return await this.usersRepository.CreateAsync(user.ToTableRecord());
        }
    }
}
