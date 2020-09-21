using System;
using System.Collections.Generic;
using System.Text;

namespace BeltsAndLeaders.Server.Business.Models.Users.UpdateUser
{
    public class UpdateUserCommandRequestModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string SpecialistArea { get; set; }
    }
}
