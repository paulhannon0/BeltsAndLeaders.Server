using BeltsAndLeaders.Server.Business.Models.MaturityCategories.UpdateMaturityCategory;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityCategories.UpdateMaturityCategory
{
    public interface IUpdateMaturityCategoryCommand : ICommand<UpdateMaturityCategoryCommandRequestModel, ulong> { }
}
