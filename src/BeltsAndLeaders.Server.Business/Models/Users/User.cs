using System;
using BeltsAndLeaders.Server.Common.Enums;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Business.Models.Users
{
    public class User
    {
        public ulong Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int TotalMaturityPoints { get; set; }

        public BeltType Belt { get; set; }

        public string SpecialistArea { get; set; }

        public DateTimeOffset? ChampionStartDate { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static User FromTableRecord(UserRecord userRecord)
        {
            DateTimeOffset? updatedAt = null;

            if (userRecord.UpdatedAt != null)
            {
                updatedAt = DateTimeOffset.FromUnixTimeMilliseconds(userRecord.UpdatedAt.Value);
            }

            Enum.TryParse(userRecord.Belt, out BeltType belt);

            return new User
            {
                Id = userRecord.Id,
                Name = userRecord.Name,
                Email = userRecord.Email,
                TotalMaturityPoints = userRecord.TotalMaturityPoints,
                Belt = belt,
                SpecialistArea = userRecord.SpecialistArea,
                ChampionStartDate = DateTimeOffset.FromUnixTimeMilliseconds(userRecord.ChampionStartDate),
                CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(userRecord.CreatedAt),
                UpdatedAt = updatedAt
            };
        }

        public UserRecord ToTableRecord()
        {
            long championStartDate = this.ChampionStartDate is null
                ? DateTimeOffset.Now.ToUnixTimeMilliseconds()
                : this.ChampionStartDate.Value.ToUnixTimeMilliseconds();

            return new UserRecord
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email,
                TotalMaturityPoints = this.TotalMaturityPoints,
                Belt = this.Belt.ToString(),
                SpecialistArea = this.SpecialistArea,
                ChampionStartDate = championStartDate
            };
        }
    }
}
