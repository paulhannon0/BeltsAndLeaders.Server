using System.Collections.Generic;
using BeltsAndLeaders.Server.Api.Models.Users.GetUser;
using BeltsAndLeaders.Server.Business.Models.Users;

namespace BeltsAndLeaders.Server.Api.Models.Users.GetAllUsers
{
    public class GetAllUsersResponseModel
    {
        public List<GetUserResponseModel> Users { get; set; }

        public static GetAllUsersResponseModel FromBusinessModel(IEnumerable<User> users)
        {
            var responseUsers = new List<GetUserResponseModel>();

            foreach (var user in users)
            {
                responseUsers.Add
                (
                    new GetUserResponseModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        MaturityLevel = user.MaturityLevel,
                        Belt = user.Belt,
                        SpecialistArea = user.SpecialistArea,
                        ChampionStartDate = user.ChampionStartDate.Value,
                        CreatedAt = user.CreatedAt,
                        UpdatedAt = user.UpdatedAt
                    }
                );
            }

            return new GetAllUsersResponseModel
            {
                Users = responseUsers
            };
        }
    }
}
