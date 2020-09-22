using System;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Common.Enums;

namespace BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetMaturityLevel
{
    public class GetMaturityLevelResponseModel
    {
        public Guid Id { get; set; }

        public Guid MaturityCategoryId { get; set; }

        public BeltType BeltLevel { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static GetMaturityLevelResponseModel FromBusinessModel(MaturityLevel maturityLevel)
        {
            return new GetMaturityLevelResponseModel
            {
                Id = maturityLevel.Id,
                MaturityCategoryId = maturityLevel.MaturityCategoryId,
                BeltLevel = maturityLevel.BeltLevel,
                Description = maturityLevel.Description,
                CreatedAt = maturityLevel.CreatedAt,
                UpdatedAt = maturityLevel.UpdatedAt
            };
        }
    }
}
