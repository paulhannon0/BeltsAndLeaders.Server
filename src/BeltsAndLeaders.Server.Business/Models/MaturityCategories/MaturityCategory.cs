using System;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Business.Models.MaturityCategories
{
    public class MaturityCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static MaturityCategory FromTableRecord(MaturityCategoryRecord maturityCategoryRecord)
        {
            DateTimeOffset? updatedAt = null;

            if (maturityCategoryRecord.UpdatedAt != null)
            {
                updatedAt = DateTimeOffset.FromUnixTimeMilliseconds(maturityCategoryRecord.UpdatedAt.Value);
            }

            return new MaturityCategory
            {
                Id = new Guid(maturityCategoryRecord.Id),
                Name = maturityCategoryRecord.Name,
                CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(maturityCategoryRecord.CreatedAt),
                UpdatedAt = updatedAt
            };
        }

        public MaturityCategoryRecord ToTableRecord()
        {
            return new MaturityCategoryRecord
            {
                Id = this.Id.ToByteArray(),
                Name = this.Name
            };
        }
    }
}
