using System.Collections.Generic;
using BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetMaturityCategory;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories;

namespace BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetAllMaturityCategories
{
    public class GetAllMaturityCategoriesResponseModel
    {
        public List<GetMaturityCategoryResponseModel> MaturityCategories { get; set; }

        public static GetAllMaturityCategoriesResponseModel FromBusinessModel(IEnumerable<MaturityCategory> maturityCategories)
        {
            var responseMaturityCategories = new List<GetMaturityCategoryResponseModel>();

            foreach (var maturityCategory in maturityCategories)
            {
                responseMaturityCategories.Add
                (
                    new GetMaturityCategoryResponseModel
                    {
                        Id = maturityCategory.Id,
                        Name = maturityCategory.Name,
                        CreatedAt = maturityCategory.CreatedAt,
                        UpdatedAt = maturityCategory.UpdatedAt
                    }
                );
            }

            return new GetAllMaturityCategoriesResponseModel
            {
                MaturityCategories = responseMaturityCategories
            };
        }
    }
}
