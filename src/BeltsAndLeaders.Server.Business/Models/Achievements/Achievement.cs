using System;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Business.Models.Achievements
{
    public class Achievement
    {
        public ulong Id { get; set; }

        public ulong UserId { get; set; }

        public ulong MaturityLevelId { get; set; }

        public string Comment { get; set; }

        public DateTimeOffset AchievementDate { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static Achievement FromTableRecord(AchievementRecord achievementRecord)
        {
            DateTimeOffset? updatedAt = null;

            if (achievementRecord.UpdatedAt != null)
            {
                updatedAt = DateTimeOffset.FromUnixTimeMilliseconds(achievementRecord.UpdatedAt.Value);
            }

            return new Achievement
            {
                Id = achievementRecord.Id,
                UserId = achievementRecord.UserId,
                MaturityLevelId = achievementRecord.MaturityLevelId,
                AchievementDate = DateTimeOffset.FromUnixTimeMilliseconds(achievementRecord.AchievementDate),
                Comment = achievementRecord.Comment,
                CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(achievementRecord.CreatedAt),
                UpdatedAt = updatedAt
            };
        }

        public AchievementRecord ToTableRecord()
        {
            return new AchievementRecord
            {
                Id = this.Id,
                UserId = this.UserId,
                MaturityLevelId = this.MaturityLevelId,
                AchievementDate = this.AchievementDate.ToUnixTimeMilliseconds(),
                Comment = this.Comment,
            };
        }
    }
}
