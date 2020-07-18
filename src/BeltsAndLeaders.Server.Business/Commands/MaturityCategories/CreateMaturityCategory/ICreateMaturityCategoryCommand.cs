using BeltsAndLeaders.Server.Business.Models.MaturityCategories.CreateMaturityCategory;

namespace BeltsAndLeaders.Server.Business.Commands.MaturityCategories.CreateMaturityCategory
{
    public interface ICreateMaturityCategoryCommand : ICommand<CreateMaturityCategoryCommandRequestModel, ulong> { }
}
