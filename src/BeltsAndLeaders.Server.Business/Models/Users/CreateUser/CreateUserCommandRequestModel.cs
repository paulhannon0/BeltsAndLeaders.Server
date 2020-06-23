using System;
using BeltsAndLeaders.Server.Common.Enums;

namespace BeltsAndLeaders.Server.Business.Models.Users.CreateUser
{
    public class CreateUserCommandRequestModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string SpecialistArea { get; set; }

        public DateTimeOffset? ChampionStartDate { get; set; }
    }
}
