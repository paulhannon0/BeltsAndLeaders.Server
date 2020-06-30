using System;
using BeltsAndLeaders.Server.Business.Models.Users;
using BeltsAndLeaders.Server.Common.Enums;

namespace BeltsAndLeaders.Server.Api.Models.Users.GetUser
{
    public class GetUserResponseModel
    {
        public ulong Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public byte MaturityLevel { get; set; }

        public BeltType Belt { get; set; }

        public string SpecialistArea { get; set; }

        public DateTimeOffset ChampionStartDate { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static GetUserResponseModel FromBusinessModel(User user)
        {
            return new GetUserResponseModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                MaturityLevel = user.MaturityLevel,
                Belt = user.Belt,
                SpecialistArea = user.SpecialistArea,
                ChampionStartDate = user.ChampionStartDate.Value,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
