using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.GetMaturityLevelsByCategoryId;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetMaturityLevelsByCategoryId
{
    public class GetMaturityLevelsByCategoryIdQuery : IGetMaturityLevelsByCategoryIdQuery
    {
        private readonly IMaturityCategoriesRepository maturityCategoriesRepository;
        private readonly IMaturityLevelsRepository maturityLevelsRepository;

        public GetMaturityLevelsByCategoryIdQuery(
            IMaturityCategoriesRepository maturityCategoriesRepository,
            IMaturityLevelsRepository maturityLevelsRepository
        )
        {
            this.maturityCategoriesRepository = maturityCategoriesRepository;
            this.maturityLevelsRepository = maturityLevelsRepository;
        }

        public async Task<IEnumerable<MaturityLevel>> ExecuteAsync(GetMaturityLevelsByCategoryIdQueryRequestModel queryRequest)
        {
            var maturityCategory = await this.maturityCategoriesRepository.GetAsync(queryRequest.CategoryId);

            if (maturityCategory is null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityCategory (ID: {queryRequest.CategoryId}) cannot be found.");
            }

            var maturityLevelRecords = await this.maturityLevelsRepository.GetByCategoryIdAsync(queryRequest.CategoryId);
            var maturityLevels = new List<MaturityLevel>();

            foreach (var maturityLevelRecord in maturityLevelRecords)
            {
                maturityLevels.Add(MaturityLevel.FromTableRecord(maturityLevelRecord));
            }

            return maturityLevels;
        }
    }
}
