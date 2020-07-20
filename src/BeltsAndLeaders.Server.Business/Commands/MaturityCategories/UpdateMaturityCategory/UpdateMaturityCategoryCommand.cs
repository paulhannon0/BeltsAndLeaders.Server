using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories.UpdateMaturityCategory;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityCategories.UpdateMaturityCategory
{
    public class UpdateMaturityCategoryCommand : IUpdateMaturityCategoryCommand
    {
        private readonly IMaturityCategoriesRepository maturityCategoriesRepository;

        public UpdateMaturityCategoryCommand(IMaturityCategoriesRepository maturityCategoriesRepository)
        {
            this.maturityCategoriesRepository = maturityCategoriesRepository;
        }

        public async Task<ulong> ExecuteAsync(UpdateMaturityCategoryCommandRequestModel commandRequest)
        {
            var existingMaturityCategory = await this.maturityCategoriesRepository.GetAsync(commandRequest.Id);

            if (existingMaturityCategory == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityCategory (ID: {commandRequest.Id}) cannot be found.");
            }

            existingMaturityCategory.Name = commandRequest.Name;

            await this.maturityCategoriesRepository.UpdateAsync(existingMaturityCategory);

            return existingMaturityCategory.Id;
        }
    }
}
