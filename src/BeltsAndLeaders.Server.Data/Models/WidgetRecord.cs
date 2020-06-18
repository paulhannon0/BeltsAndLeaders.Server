using System;
using Dapper.Contrib.Extensions;

namespace BeltsAndLeaders.Server.Data.Models
{
    [Table("Widgets")]
    public class WidgetRecord : IRecord
    {
        [Key]
        public ulong Id { get; set; }

        public string Name { get; set; }

        public long CreatedAt { get; set; }

        public long? UpdatedAt { get; set; }
    }
}
