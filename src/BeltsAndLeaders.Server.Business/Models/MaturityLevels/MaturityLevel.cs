using System;
using BeltsAndLeaders.Server.Common.Enums;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Business.Models.MaturityLevels
{
    public class MaturityLevel
    {
        public Guid Id { get; set; }

        public Guid MaturityCategoryId { get; set; }

        public BeltType BeltLevel { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static MaturityLevel FromTableRecord(MaturityLevelRecord maturityLevelRecord)
        {
            DateTimeOffset? updatedAt = null;

            if (maturityLevelRecord.UpdatedAt != null)
            {
                updatedAt = DateTimeOffset.FromUnixTimeMilliseconds(maturityLevelRecord.UpdatedAt.Value);
            }

            return new MaturityLevel
            {
                Id = maturityLevelRecord.Id,
                MaturityCategoryId = maturityLevelRecord.MaturityCategoryId,
                BeltLevel = (BeltType)Enum.Parse(typeof(BeltType), maturityLevelRecord.BeltLevel),
                Description = maturityLevelRecord.Description,
                CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(maturityLevelRecord.CreatedAt),
                UpdatedAt = updatedAt
            };
        }

        public MaturityLevelRecord ToTableRecord()
        {
            return new MaturityLevelRecord
            {
                Id = this.Id,
                MaturityCategoryId = this.MaturityCategoryId,
                BeltLevel = this.BeltLevel.ToString(),
                Description = this.Description
            };
        }
    }
}
