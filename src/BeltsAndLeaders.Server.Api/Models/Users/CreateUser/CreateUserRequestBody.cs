using BeltsAndLeaders.Server.Business.Models.Users.CreateUser;
using BeltsAndLeaders.Server.Common.Enums;

namespace BeltsAndLeaders.Server.Api.Models.Users.CreateUser
{
    public class CreateUserRequestBody
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public byte MaturityLevel { get; set; }

        public BeltType Belt { get; set; }

        public CreateUserCommandRequestModel ToCommandRequest()
        {
            return new CreateUserCommandRequestModel
            {
                Name = this.Name,
                Email = this.Email,
                MaturityLevel = this.MaturityLevel,
                Belt = this.Belt
            };
        }
    }
}
