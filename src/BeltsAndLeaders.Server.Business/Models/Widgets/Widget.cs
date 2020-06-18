using System;
using BeltsAndLeaders.Server.Data.Models;

namespace BeltsAndLeaders.Server.Business.Models.Widgets
{
    public class Widget
    {
        public ulong Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public static Widget FromTableRecord(WidgetRecord widgetRecord)
        {
            DateTimeOffset? updatedAt = null;

            if (widgetRecord.UpdatedAt != null)
            {
                updatedAt = DateTimeOffset.FromUnixTimeMilliseconds(widgetRecord.UpdatedAt.Value);
            }

            return new Widget
            {
                Id = widgetRecord.Id,
                Name = widgetRecord.Name,
                CreatedAt = DateTimeOffset.FromUnixTimeMilliseconds(widgetRecord.CreatedAt),
                UpdatedAt = updatedAt
            };
        }

        public WidgetRecord ToTableRecord()
        {
            return new WidgetRecord
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }
}
