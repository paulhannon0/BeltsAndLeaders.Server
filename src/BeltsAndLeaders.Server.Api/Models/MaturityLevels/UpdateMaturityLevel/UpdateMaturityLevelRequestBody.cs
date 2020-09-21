using System;
using BeltsAndLeaders.Server.Business.Models.MaturityLevels.UpdateMaturityLevel;

namespace BeltsAndLeaders.Server.Api.Models.MaturityLevels.UpdateMaturityLevel
{
    public class UpdateMaturityLevelRequestBody
    {
        public string Description { get; set; }

        public UpdateMaturityLevelCommandRequestModel ToCommandRequest(Guid id)
        {
            return new UpdateMaturityLevelCommandRequestModel
            {
                Id = id,
                Description = this.Description
            };
        }
    }
}
