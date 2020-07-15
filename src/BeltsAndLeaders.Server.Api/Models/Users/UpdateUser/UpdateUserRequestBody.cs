using BeltsAndLeaders.Server.Business.Models.Users.UpdateUser;

namespace BeltsAndLeaders.Server.Api.Models.Users.UpdateUser
{
    public class UpdateUserRequestBody
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string SpecialistArea { get; set; }

        public UpdateUserCommandRequestModel ToCommandRequest(ulong id)
        {
            return new UpdateUserCommandRequestModel
            {
                Id = id,
                Name = this.Name,
                SpecialistArea = SpecialistArea,
                Email = Email

            };
        }
    }
}
