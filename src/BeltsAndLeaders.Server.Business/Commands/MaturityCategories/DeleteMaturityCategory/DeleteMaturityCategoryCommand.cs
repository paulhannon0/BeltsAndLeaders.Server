using System;
using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories.DeleteMaturityCategory;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityCategories.DeleteMaturityCategory
{
    public class DeleteMaturityCategoryCommand : IDeleteMaturityCategoryCommand
    {
        private readonly IMaturityCategoriesRepository maturityCategoriesRepository;

        public DeleteMaturityCategoryCommand(IMaturityCategoriesRepository maturityCategoriesRepository)
        {
            this.maturityCategoriesRepository = maturityCategoriesRepository;
        }

        public async Task<Guid> ExecuteAsync(DeleteMaturityCategoryCommandRequestModel commandRequest)
        {
            var maturityCategory = await this.maturityCategoriesRepository.GetAsync(commandRequest.Id);

            if (maturityCategory == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityCategory (ID: {commandRequest.Id}) cannot be found.");
            }

            await this.maturityCategoriesRepository.DeleteAsync(commandRequest.Id);

            return commandRequest.Id;
        }
    }
}
