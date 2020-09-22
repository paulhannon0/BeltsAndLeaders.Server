using System;

namespace BeltsAndLeaders.Server.Business.Models.MaturityCategories.UpdateMaturityCategory
{
    public class UpdateMaturityCategoryCommandRequestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
