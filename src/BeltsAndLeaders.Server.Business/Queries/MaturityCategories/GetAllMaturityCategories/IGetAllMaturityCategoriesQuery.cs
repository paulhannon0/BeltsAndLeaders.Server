using System.Collections.Generic;
using BeltsAndLeaders.Server.Business.Models.MaturityCategories;

namespace BeltsAndLeaders.Server.Business.Queries.MaturityCategories.GetAllMaturityCategories
{
    public interface IGetAllMaturityCategoriesQuery : IQueryResponseOnly<IEnumerable<MaturityCategory>> { }
}
