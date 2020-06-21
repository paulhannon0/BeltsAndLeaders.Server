using BeltsAndLeaders.Server.Data.Extensions;
using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(20200618184300)]
    public class CreateUsersTable20200618184300 : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists
            (
                "Users",
                table => table
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("Name").AsFixedLengthString(255)
                    .WithColumn("Email").AsFixedLengthString(255)
                    .WithColumn("MaturityLevel").AsByte()
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
