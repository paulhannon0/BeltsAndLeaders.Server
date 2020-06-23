using System;
using BeltsAndLeaders.Server.Business.Models.Users.CreateUser;

namespace BeltsAndLeaders.Server.Api.Models.Users.CreateUser
{
    public class CreateUserRequestBody
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string SpecialistArea { get; set; }

        public DateTimeOffset? ChampionStartDate { get; set; }


        public CreateUserCommandRequestModel ToCommandRequest()
        {
            return new CreateUserCommandRequestModel
            {
                Name = this.Name,
                Email = this.Email,
                SpecialistArea = this.SpecialistArea,
                ChampionStartDate = this.ChampionStartDate
            };
        }
    }
}
