using System;
using BeltsAndLeaders.Server.Common.Enums;
using Dapper.Contrib.Extensions;

namespace BeltsAndLeaders.Server.Data.Models
{
    [Table("Users")]
    public class UserRecord : IRecord
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int TotalMaturityPoints { get; set; }

        public string Belt { get; set; }

        public string SpecialistArea { get; set; }

        public long ChampionStartDate { get; set; }

        public long CreatedAt { get; set; }

        public long? UpdatedAt { get; set; }
    }
}
