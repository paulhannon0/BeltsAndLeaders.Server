using System.Net;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.CreateMaturityLevel;
using BeltsAndLeaders.Server.Common.Exceptions;
using BeltsAndLeaders.Server.Data.Repositories;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityLevels.CreateMaturityLevel
{
    public class CreateMaturityLevelCommand : ICreateMaturityLevelCommand
    {
        private readonly IMaturityLevelsRepository maturityLevelsRepository;
        private readonly IMaturityCategoriesRepository maturityCategoriesRepository;

        public CreateMaturityLevelCommand(
            IMaturityLevelsRepository maturityLevelsRepository,
            IMaturityCategoriesRepository maturityCategoriesRepository
        )
        {
            this.maturityLevelsRepository = maturityLevelsRepository;
            this.maturityCategoriesRepository = maturityCategoriesRepository;
        }

        public async Task<ulong> ExecuteAsync(CreateMaturityLevelCommandRequestModel commandRequest)
        {
            var maturityCategory = await this.maturityCategoriesRepository.GetAsync(commandRequest.MaturityCategoryId);

            if (maturityCategory == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"MaturityCategory (ID: {commandRequest.MaturityCategoryId}) cannot be found.");
            }

            var maturityLevel = new MaturityLevel
            {
                MaturityCategoryId = commandRequest.MaturityCategoryId,
                BeltLevel = commandRequest.BeltLevel,
                Description = commandRequest.Description
            };

            return await this.maturityLevelsRepository.CreateAsync(maturityLevel.ToTableRecord());
        }
    }
}
