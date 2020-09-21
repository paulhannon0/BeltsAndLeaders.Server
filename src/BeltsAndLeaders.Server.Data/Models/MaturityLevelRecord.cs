using System;
using BeltsAndLeaders.Server.Common.Enums;
using Dapper.Contrib.Extensions;

namespace BeltsAndLeaders.Server.Data.Models
{
    [Table("MaturityLevels")]
    public class MaturityLevelRecord : IRecord
    {
        [Key]
        public Guid Id { get; set; }

        public Guid MaturityCategoryId { get; set; }

        public string BeltLevel { get; set; }

        public string Description { get; set; }

        public long CreatedAt { get; set; }

        public long? UpdatedAt { get; set; }
    }
}
