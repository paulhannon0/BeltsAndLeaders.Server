using System;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.CreateMaturityLevel;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityLevels.CreateMaturityLevel
{
    public interface ICreateMaturityLevelCommand : ICommand<CreateMaturityLevelCommandRequestModel, Guid> { }
}
