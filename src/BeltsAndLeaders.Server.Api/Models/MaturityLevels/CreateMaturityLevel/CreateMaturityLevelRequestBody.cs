using System.ComponentModel.DataAnnotations;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.CreateMaturityLevel;

namespace BeltsAndLeaders.Server.Api.Models.MaturityLevels.CreateMaturityLevel
{
    public class CreateMaturityLevelRequestBody
    {
        [Required]
        public ulong MaturityCategoryId { get; set; }

        [Required]
        public byte MaturityLevel { get; set; }

        [Required]
        public string Description { get; set; }

        public CreateMaturityLevelCommandRequestModel ToCommandRequest()
        {
            return new CreateMaturityLevelCommandRequestModel
            {
                MaturityCategoryId = this.MaturityCategoryId,
                MaturityLevel = this.MaturityLevel,
                Description = this.Description
            };
        }
    }
}
