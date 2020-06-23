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

        public byte MaturityLevel { get; set; }

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

            return new User
            {
                Id = userRecord.Id,
                Name = userRecord.Name,
                Email = userRecord.Email,
                MaturityLevel = userRecord.MaturityLevel,
                Belt = userRecord.Belt,
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
                SpecialistArea = this.SpecialistArea,
                ChampionStartDate = championStartDate
            };
        }
    }
}
