using System;
using System.ComponentModel.DataAnnotations;
using BeltsAndLeaders.Server.Business.Models.Achievements.CreateAchievement;

namespace BeltsAndLeaders.Server.Api.Models.Achievements.CreateAchievement
{
    public class CreateAchievementRequestBody
    {
        [Required]
        public ulong UserId { get; set; }

        [Required]
        public ulong MaturityLevelId { get; set; }

        [Required]
        public DateTimeOffset AchievementDate { get; set; }

        [Required]
        public string Comment { get; set; }

        public CreateAchievementCommandRequestModel ToCommandRequest()
        {
            return new CreateAchievementCommandRequestModel
            {
                UserId = this.UserId,
                MaturityLevelId = this.MaturityLevelId,
                AchievementDate = this.AchievementDate,
                Comment = this.Comment
            };
        }
    }
}
