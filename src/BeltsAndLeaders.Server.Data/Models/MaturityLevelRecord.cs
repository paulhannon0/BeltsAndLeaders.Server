using Dapper.Contrib.Extensions;

namespace BeltsAndLeaders.Server.Data.Models
{
    [Table("MaturityLevels")]
    public class MaturityLevelRecord : IRecord
    {
        [Key]
        public ulong Id { get; set; }

        [ExplicitKey]
        public ulong MaturityCategoryId { get; set; }

        public byte MaturityLevel { get; set; }

        public string Description { get; set; }

        public long CreatedAt { get; set; }

        public long? UpdatedAt { get; set; }
    }
}
