using BeltsAndLeaders.Server.Business.Models.MaturityLevels.DeleteMaturityLevel;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityLevels.DeleteMaturityLevel
{
    public interface IDeleteMaturityLevelCommand : ICommand<DeleteMaturityLevelCommandRequestModel, ulong> { }
}
