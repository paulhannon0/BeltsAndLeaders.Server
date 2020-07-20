using System;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Business.Models.MaturityLevels
{
    public class MaturityLevel
    {
        public ulong Id { get; set; }

        public ulong MaturityCategoryId { get; set; }

        public byte Level { get; set; }

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
                Level = maturityLevelRecord.MaturityLevel,
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
                MaturityLevel = this.Level,
                Description = this.Description
            };
        }
    }
}
