using System;

namespace BeltsAndLeaders.Server.Business.Models.MaturityLevels.UpdateMaturityLevel
{
    public class UpdateMaturityLevelCommandRequestModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}
