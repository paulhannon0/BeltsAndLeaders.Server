using System.ComponentModel.DataAnnotations;
using BeltsAndLeaders.Server.Api.Attributes;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.CreateMaturityLevel;
using BeltsAndLeaders.Server.Common.Enums;

namespace BeltsAndLeaders.Server.Api.Models.MaturityLevels.CreateMaturityLevel
{
    public class CreateMaturityLevelRequestBody
    {
        [Required]
        public ulong MaturityCategoryId { get; set; }

        [RequiredEnum]
        public BeltType BeltLevel { get; set; }

        [Required]
        public string Description { get; set; }

        public CreateMaturityLevelCommandRequestModel ToCommandRequest()
        {
            return new CreateMaturityLevelCommandRequestModel
            {
                MaturityCategoryId = this.MaturityCategoryId,
                BeltLevel = this.BeltLevel,
                Description = this.Description
            };
        }
    }
}
