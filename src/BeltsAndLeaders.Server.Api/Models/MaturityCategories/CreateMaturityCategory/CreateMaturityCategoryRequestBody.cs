using System.ComponentModel.DataAnnotations;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories.CreateMaturityCategory;

namespace BeltsAndLeaders.Server.Api.Models.MaturityCategories.CreateMaturityCategory
{
    public class CreateMaturityCategoryRequestBody
    {
        [Required]
        public string Name { get; set; }

        public CreateMaturityCategoryCommandRequestModel ToCommandRequest()
        {
            return new CreateMaturityCategoryCommandRequestModel
            {
                Name = this.Name
            };
        }
    }
}
