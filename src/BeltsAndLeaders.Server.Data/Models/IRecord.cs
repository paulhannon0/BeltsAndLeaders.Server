using System;

namespace BeltsAndLeaders.Server.Data.Models
{
    public interface IRecord
    {
        long CreatedAt { get; set; }

        long? UpdatedAt { get; set; }
    }
}
