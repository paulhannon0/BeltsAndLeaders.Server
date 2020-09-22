using System;

namespace BeltsAndLeaders.Server.Api.Models.Users.CreateUser
{
    public class CreateUserResponseModel
    {
        public Guid Id { get; set; }

        public static CreateUserResponseModel FromBusinessModel(Guid id)
        {
            return new CreateUserResponseModel
            {
                Id = id
            };
        }
    }
}
