using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityCategories.GetAllMaturityCategories
{
    public class GetAllMaturityCategoriesQuery : IGetAllMaturityCategoriesQuery
    {
        private readonly IMaturityCategoriesRepository maturityCategoriesRepository;

        public GetAllMaturityCategoriesQuery(IMaturityCategoriesRepository maturityCategoriesRepository)
        {
            this.maturityCategoriesRepository = maturityCategoriesRepository;
        }

        public async Task<IEnumerable<MaturityCategory>> ExecuteAsync()
        {
            var maturityCategories = await this.maturityCategoriesRepository.GetAllAsync();
            var maturityCategoryList = new List<MaturityCategory>();

            foreach (var maturityCategory in maturityCategories)
            {
                maturityCategoryList.Add(MaturityCategory.FromTableRecord(maturityCategory));
            }

            return maturityCategoryList;
        }
    }
}
