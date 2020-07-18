using BeltsAndLeaders.Server.Business.Models.MaturityCategories;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories.GetMaturityCategory;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityCategories.GetMaturityCategory
{
    public interface IGetMaturityCategoryQuery : IQuery<GetMaturityCategoryQueryRequestModel, MaturityCategory> { }
}
