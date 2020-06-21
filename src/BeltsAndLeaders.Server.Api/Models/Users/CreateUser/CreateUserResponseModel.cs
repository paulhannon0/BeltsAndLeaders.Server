namespace BeltsAndLeaders.Server.Api.Models.Users.CreateUser
{
    public class CreateUserResponseModel
    {
        public ulong Id { get; set; }

        public static CreateUserResponseModel FromBusinessModel(ulong id)
        {
            return new CreateUserResponseModel
            {
                Id = id
            };
        }
    }
}
