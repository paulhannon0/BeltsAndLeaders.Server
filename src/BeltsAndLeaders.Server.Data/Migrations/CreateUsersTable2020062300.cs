using BeltsAndLeaders.Server.Data.Extensions;
using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(2020062300)]
    public class CreateUsersTable2020062300 : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists
            (
                "Users",
                table => table
                    .WithColumn("Id").AsBinary(16).PrimaryKey()
                    .WithColumn("Name").AsFixedLengthString(255)
                    .WithColumn("Email").AsFixedLengthString(255)
                    .WithColumn("TotalMaturityPoints").AsInt32()
                    .WithColumn("Belt").AsFixedLengthString(255)
                    .WithColumn("SpecialistArea").AsFixedLengthString(255).Nullable()
                    .WithColumn("ChampionStartDate").AsInt64()
                    .WithColumn("CreatedAt").AsInt64()
                    .WithColumn("UpdatedAt").AsInt64().Nullable()
            );
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
