namespace BeltsAndLeaders.Server.Api.Models.MaturityCategories.CreateMaturityCategory
{
    public class CreateMaturityCategoryResponseModel
    {
        public ulong Id { get; set; }

        public static CreateMaturityCategoryResponseModel FromBusinessModel(ulong id)
        {
            return new CreateMaturityCategoryResponseModel
            {
                Id = id
            };
        }
    }
}
