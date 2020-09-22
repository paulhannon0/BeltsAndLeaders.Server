using System;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories.UpdateMaturityCategory;

namespace BeltsAndLeaders.Server.Api.Models.MaturityCategories.UpdateMaturityCategory
{
    public class UpdateMaturityCategoryRequestBody
    {
        public string Name { get; set; }

        public UpdateMaturityCategoryCommandRequestModel ToCommandRequest(Guid id)
        {
            return new UpdateMaturityCategoryCommandRequestModel
            {
                Id = id,
                Name = this.Name
            };
        }
    }
}
