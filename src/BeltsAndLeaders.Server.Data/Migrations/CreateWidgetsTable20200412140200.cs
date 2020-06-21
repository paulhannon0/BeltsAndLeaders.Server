using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(20200412140200)]
    public class CreateWidgetsTable20200412140200 : Migration
    {
        public override void Up()
        {
            this.Execute.Sql($"DROP TABLE IF EXISTS [BeltsAndLeaders].[Widgets];");

            Create.Table("Widgets")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsFixedLengthString(255)
                .WithColumn("CreatedAt").AsInt64()
                .WithColumn("UpdatedAt").AsInt64().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Widgets");
        }
    }
}
