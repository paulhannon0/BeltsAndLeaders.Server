using System;
using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.UpdateMaturityLevel;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityLevels.UpdateMaturityLevel
{
    public class UpdateMaturityLevelCommand : IUpdateMaturityLevelCommand
    {
        private readonly IMaturityLevelsRepository maturityLevelsRepository;

        public UpdateMaturityLevelCommand(IMaturityLevelsRepository maturityLevelsRepository)
        {
            this.maturityLevelsRepository = maturityLevelsRepository;
        }

        public async Task<Guid> ExecuteAsync(UpdateMaturityLevelCommandRequestModel commandRequest)
        {
            var existingMaturityLevel = await this.maturityLevelsRepository.GetAsync(commandRequest.Id);

            if (existingMaturityLevel == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityLevel (ID: {commandRequest.Id}) cannot be found.");
            }

            existingMaturityLevel.Description = commandRequest.Description;

            await this.maturityLevelsRepository.UpdateAsync(existingMaturityLevel);

            return existingMaturityLevel.Id;
        }
    }
}
