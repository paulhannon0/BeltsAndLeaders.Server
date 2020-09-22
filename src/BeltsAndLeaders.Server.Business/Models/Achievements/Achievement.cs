using System;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Business.Models.Achievements
{
    public class Achievement
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid MaturityLevelId { get; set; }

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
                Id = new Guid(achievementRecord.Id),
                UserId = new Guid(achievementRecord.UserId),
                MaturityLevelId = new Guid(achievementRecord.MaturityLevelId),
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
                Id = this.Id.ToByteArray(),
                UserId = this.UserId.ToByteArray(),
                MaturityLevelId = this.MaturityLevelId.ToByteArray(),
                AchievementDate = this.AchievementDate.ToUnixTimeMilliseconds(),
                Comment = this.Comment
            };
        }
    }
}
