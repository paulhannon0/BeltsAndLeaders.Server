using BeltsAndLeaders.Server.Data.Extensions;
using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(20200412140200)]
    public class CreateWidgetsTable20200412140200 : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists
            (
                "Widgets",
                table => table
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Name").AsFixedLengthString(255)
                    .WithColumn("CreatedAt").AsInt64()
                    .WithColumn("UpdatedAt").AsInt64().Nullable()
            );
        }

        public override void Down()
        {
            Delete.Table("Widgets");
        }
    }
}
