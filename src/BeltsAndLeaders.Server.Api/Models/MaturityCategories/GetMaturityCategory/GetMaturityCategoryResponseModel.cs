using System;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories;

namespace BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetMaturityCategory
{
    public class GetMaturityCategoryResponseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static GetMaturityCategoryResponseModel FromBusinessModel(MaturityCategory maturityCategory)
        {
            return new GetMaturityCategoryResponseModel
            {
                Id = maturityCategory.Id,
                Name = maturityCategory.Name,
                CreatedAt = maturityCategory.CreatedAt,
                UpdatedAt = maturityCategory.UpdatedAt
            };
        }
    }
}
