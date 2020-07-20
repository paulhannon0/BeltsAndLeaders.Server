namespace BeltsAndLeaders.Server.Business.Models.MaturityLevels.CreateMaturityLevel
{
    public class CreateMaturityLevelCommandRequestModel
    {
        public ulong MaturityCategoryId { get; set; }

        public byte MaturityLevel { get; set; }

        public string Description { get; set; }
    }
}
