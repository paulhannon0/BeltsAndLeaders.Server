using System.Collections.Generic;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityLevels.GetAllMaturityLevels
{
    public interface IGetAllMaturityLevelsQuery : IQueryResponseOnly<IEnumerable<MaturityLevel>> { }
}
