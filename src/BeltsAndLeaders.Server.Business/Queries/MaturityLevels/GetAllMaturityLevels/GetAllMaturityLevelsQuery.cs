using System.Collections.Generic;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetAllMaturityLevels
{
    public class GetAllMaturityLevelsQuery : IGetAllMaturityLevelsQuery
    {
        private readonly IMaturityLevelsRepository maturityLevelsRepository;

        public GetAllMaturityLevelsQuery(IMaturityLevelsRepository maturityLevelsRepository)
        {
            this.maturityLevelsRepository = maturityLevelsRepository;
        }

        public async Task<IEnumerable<MaturityLevel>> ExecuteAsync()
        {
            var maturityLevels = await this.maturityLevelsRepository.GetAllAsync();
            var maturityLevelList = new List<MaturityLevel>();

            foreach (var maturityLevel in maturityLevels)
            {
                maturityLevelList.Add(MaturityLevel.FromTableRecord(maturityLevel));
            }

            return maturityLevelList;
        }
    }
}
