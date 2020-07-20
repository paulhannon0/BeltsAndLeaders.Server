using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories.GetMaturityCategory;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityCategories.GetMaturityCategory
{
    public class GetMaturityCategoryQuery : IGetMaturityCategoryQuery
    {
        private readonly IMaturityCategoriesRepository maturityCategoriesRepository;

        public GetMaturityCategoryQuery(IMaturityCategoriesRepository maturityCategoriesRepository)
        {
            this.maturityCategoriesRepository = maturityCategoriesRepository;
        }

        public async Task<MaturityCategory> ExecuteAsync(GetMaturityCategoryQueryRequestModel queryRequest)
        {
            var maturityCategory = await this.maturityCategoriesRepository.GetAsync(queryRequest.Id);

            if (maturityCategory == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityCategory (ID: {queryRequest.Id}) cannot be found.");
            }

            return MaturityCategory.FromTableRecord(maturityCategory);
        }
    }
}
