using BeltsAndLeaders.Server.Common.Enums;
using Dapper.Contrib.Extensions;

namespace BeltsAndLeaders.Server.Data.Models
{
    [Table("Users")]
    public class UserRecord : IRecord
    {
        [Key]
        public ulong Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public byte MaturityLevel { get; set; }

        public BeltType Belt { get; set; }

        public string SpecialistArea { get; set; }

        public long ChampionStartDate { get; set; }

        public long CreatedAt { get; set; }

        public long? UpdatedAt { get; set; }
    }
}
