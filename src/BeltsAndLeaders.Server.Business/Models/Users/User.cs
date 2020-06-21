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
                CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(userRecord.CreatedAt),
                UpdatedAt = updatedAt
            };
        }

        public UserRecord ToTableRecord()
        {
            return new UserRecord
            {
                Id = this.Id,
                Name = this.Name,
                Email = this.Email,
                MaturityLevel = this.MaturityLevel,
                Belt = this.Belt
            };
        }
    }
}
