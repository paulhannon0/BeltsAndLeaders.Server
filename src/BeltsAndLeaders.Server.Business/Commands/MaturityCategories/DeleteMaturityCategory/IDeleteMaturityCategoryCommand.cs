using BeltsAndLeaders.Server.Business.Models.MaturityCategories.DeleteMaturityCategory;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityCategories.DeleteMaturityCategory
{
    public interface IDeleteMaturityCategoryCommand : ICommand<DeleteMaturityCategoryCommandRequestModel, ulong> { }
}
