using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.GetMaturityLevel;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetMaturityLevel
{
    public interface IGetMaturityLevelQuery : IQuery<GetMaturityLevelQueryRequestModel, MaturityLevel> { }
}
