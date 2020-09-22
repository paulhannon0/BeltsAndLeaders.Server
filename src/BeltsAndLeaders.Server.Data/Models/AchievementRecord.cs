using Dapper.Contrib.Extensions;

namespace BeltsAndLeaders.Server.Data.Models
{
    [Table("Achievements")]
    public class AchievementRecord : IRecord
    {
        [ExplicitKey]
        public byte[] Id { get; set; }

        public byte[] UserId { get; set; }

        public byte[] MaturityLevelId { get; set; }

        public long AchievementDate { get; set; }

        public string Comment { get; set; }

        public long CreatedAt { get; set; }

        public long? UpdatedAt { get; set; }
    }
}
