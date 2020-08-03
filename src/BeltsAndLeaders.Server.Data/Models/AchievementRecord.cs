using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltsAndLeaders.Server.Data.Models
{
    [Table("Achievements")]
    public class AchievementRecord : IRecord
    {
        [Key]
        public ulong Id { get; set; }

        public ulong UserId { get; set; }

        public ulong MaturityLevelId { get; set; }

        public long AchievementDate { get; set; }

        public string Comment { get; set; }

        public long CreatedAt { get; set; }

        public long? UpdatedAt { get; set; }
    }
}
