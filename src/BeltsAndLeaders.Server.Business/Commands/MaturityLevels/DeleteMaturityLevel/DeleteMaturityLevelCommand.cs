using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.DeleteMaturityLevel;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityLevels.DeleteMaturityLevel
{
    public class DeleteMaturityLevelCommand : IDeleteMaturityLevelCommand
    {
        private readonly IMaturityLevelsRepository maturityLevelsRepository;

        public DeleteMaturityLevelCommand(IMaturityLevelsRepository maturityLevelsRepository)
        {
            this.maturityLevelsRepository = maturityLevelsRepository;
        }

        public async Task<ulong> ExecuteAsync(DeleteMaturityLevelCommandRequestModel commandRequest)
        {
            var maturityLevel = await this.maturityLevelsRepository.GetAsync(commandRequest.Id);

            if (maturityLevel == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityLevel (ID: {commandRequest.Id}) cannot be found.");
            }

            await this.maturityLevelsRepository.DeleteAsync(commandRequest.Id);

            return commandRequest.Id;
        }
    }
}
