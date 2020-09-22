using System;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.UpdateMaturityLevel;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityLevels.UpdateMaturityLevel
{
    public interface IUpdateMaturityLevelCommand : ICommand<UpdateMaturityLevelCommandRequestModel, Guid> { }
}
