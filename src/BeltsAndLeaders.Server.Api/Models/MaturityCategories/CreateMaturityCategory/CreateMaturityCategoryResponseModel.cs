using System;

namespace BeltsAndLeaders.Server.Api.Models.MaturityCategories.CreateMaturityCategory
{
    public class CreateMaturityCategoryResponseModel
    {
        public Guid Id { get; set; }

        public static CreateMaturityCategoryResponseModel FromBusinessModel(Guid id)
        {
            return new CreateMaturityCategoryResponseModel
            {
                Id = id
            };
        }
    }
}
