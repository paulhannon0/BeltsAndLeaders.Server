using System.Collections.Generic;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.GetMaturityLevelsByCategoryId;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetMaturityLevelsByCategoryId
{
    public interface IGetMaturityLevelsByCategoryIdQuery : IQuery<GetMaturityLevelsByCategoryIdQueryRequestModel, IEnumerable<MaturityLevel>> { }
}
