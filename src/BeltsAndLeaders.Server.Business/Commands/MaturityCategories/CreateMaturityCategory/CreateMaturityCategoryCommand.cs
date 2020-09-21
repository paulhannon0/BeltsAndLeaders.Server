using System;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories.CreateMaturityCategory;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityCategories.CreateMaturityCategory
{
    public class CreateMaturityCategoryCommand : ICreateMaturityCategoryCommand
    {
        private readonly IMaturityCategoriesRepository maturityCategoriesRepository;

        public CreateMaturityCategoryCommand(IMaturityCategoriesRepository maturityCategoriesRepository)
        {
            this.maturityCategoriesRepository = maturityCategoriesRepository;
        }

        public async Task<Guid> ExecuteAsync(CreateMaturityCategoryCommandRequestModel commandRequest)
        {
            var maturityCategory = new MaturityCategory
            {
                Name = commandRequest.Name
            };

            return await this.maturityCategoriesRepository.CreateAsync(maturityCategory.ToTableRecord());
        }
    }
}
