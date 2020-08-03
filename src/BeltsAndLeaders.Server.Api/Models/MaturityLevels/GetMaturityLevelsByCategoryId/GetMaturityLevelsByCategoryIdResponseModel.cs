using System.Collections.Generic;
using BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetMaturityLevel;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels;

namespace BeltsAndLeaders.Server.Api.Models.MaturityLevels.GetMaturityLevelsByCategoryId
{
    public class GetMaturityLevelsByCategoryIdResponseModel
    {
        public List<GetMaturityLevelResponseModel> MaturityLevels { get; set; }

        public static GetMaturityLevelsByCategoryIdResponseModel FromBusinessModel(IEnumerable<MaturityLevel> maturityLevels)
        {
            var responseMaturityLevels = new List<GetMaturityLevelResponseModel>();

            foreach (var maturityLevel in maturityLevels)
            {
                responseMaturityLevels.Add
                (
                    new GetMaturityLevelResponseModel
                    {
                        Id = maturityLevel.Id,
                        MaturityCategoryId = maturityLevel.MaturityCategoryId,
                        MaturityLevel = maturityLevel.Level,
                        Description = maturityLevel.Description,
                        CreatedAt = maturityLevel.CreatedAt,
                        UpdatedAt = maturityLevel.UpdatedAt
                    }
                );
            }

            return new GetMaturityLevelsByCategoryIdResponseModel
            {
                MaturityLevels = responseMaturityLevels
            };
        }
    }
}
