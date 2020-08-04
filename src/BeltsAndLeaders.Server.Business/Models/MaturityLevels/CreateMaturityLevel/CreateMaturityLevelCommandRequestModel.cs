using BeltsAndLeaders.Server.Common.Enums;

namespace BeltsAndLeaders.Server.Business.Models.MaturityLevels.CreateMaturityLevel
{
    public class CreateMaturityLevelCommandRequestModel
    {
        public ulong MaturityCategoryId { get; set; }

        public BeltType BeltLevel { get; set; }

        public string Description { get; set; }
    }
}
