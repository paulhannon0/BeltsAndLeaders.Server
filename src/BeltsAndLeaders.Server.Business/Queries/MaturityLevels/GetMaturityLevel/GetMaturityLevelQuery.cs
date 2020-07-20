using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.GetMaturityLevel;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetMaturityLevel
{
    public class GetMaturityLevelQuery : IGetMaturityLevelQuery
    {
        private readonly IMaturityLevelsRepository maturityLevelsRepository;

        public GetMaturityLevelQuery(IMaturityLevelsRepository maturityLevelsRepository)
        {
            this.maturityLevelsRepository = maturityLevelsRepository;
        }

        public async Task<MaturityLevel> ExecuteAsync(GetMaturityLevelQueryRequestModel queryRequest)
        {
            var maturityLevel = await this.maturityLevelsRepository.GetAsync(queryRequest.Id);

            if (maturityLevel == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityLevel (ID: {queryRequest.Id}) cannot be found.");
            }

            return MaturityLevel.FromTableRecord(maturityLevel);
        }
    }
}
