using System;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;

namespace BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetMaturityLevel
{
    public class GetMaturityLevelResponseModel
    {
        public ulong Id { get; set; }

        public ulong MaturityCategoryId { get; set; }

        public byte MaturityLevel { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static GetMaturityLevelResponseModel FromBusinessModel(MaturityLevel maturityLevel)
        {
            return new GetMaturityLevelResponseModel
            {
                Id = maturityLevel.Id,
                MaturityCategoryId = maturityLevel.MaturityCategoryId,
                MaturityLevel = maturityLevel.Level,
                Description = maturityLevel.Description,
                CreatedAt = maturityLevel.CreatedAt,
                UpdatedAt = maturityLevel.UpdatedAt
            };
        }
    }
}
