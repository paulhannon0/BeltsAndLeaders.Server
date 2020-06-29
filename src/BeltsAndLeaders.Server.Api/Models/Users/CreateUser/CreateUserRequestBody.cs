using System;
using System.ComponentModel.DataAnnotations;
using BeltsAndLeaders.Server.Business.Models.Users.CreateUser;

namespace BeltsAndLeaders.Server.Api.Models.Users.CreateUser
{
    public class CreateUserRequestBody
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
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
