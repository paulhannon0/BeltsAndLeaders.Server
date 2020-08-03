using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Api.Models.MaturityCategories.GetMaturityCategory;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;
using BeltsAndLeaders.Server.Data.Repositories.Mysql;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public class MaturityCategoryDataHelper : TestDataHelper
    {
        private MysqlMaturityLevelsRepository maturityLevelsRepository;

        public MaturityCategoryDataHelper(
            TestHost testHost,
            MysqlMaturityLevelsRepository maturityLevelsRepository
        )
            : base(testHost)
        {
            this.maturityLevelsRepository = maturityLevelsRepository;
        }

        public async Task<ulong> CreateMaturityCategoryAsync(string name)
        {
            var requestBody = new Dictionary<string, object>()
            {
                { "Name", name }
            };

            var responseMessage = await this.TestHost.PostAsync("/maturity-categories", requestBody);

            return ulong.Parse(await responseMessage.Content.ReadAsStringAsync());
        }

        public async Task<GetMaturityCategoryResponseModel> GetMaturityCategoryAsync(ulong id)
        {
            var responseMessage = await this.TestHost.GetAsync($"/maturity-categories/{id}");

            return JsonSerializer.Deserialize<GetMaturityCategoryResponseModel>
            (
                await responseMessage.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }
            );
        }

        public async Task<IEnumerable<MaturityLevel>> GetMaturityLevelsByCategoryId(ulong categoryId)
        {
            var records = await this.maturityLevelsRepository.GetByCategoryIdAsync(categoryId);
            var entities = new List<MaturityLevel>();

            foreach (var record in records)
            {
                entities.Add(MaturityLevel.FromTableRecord(record));
            }

            return entities;
        }
    }
}
