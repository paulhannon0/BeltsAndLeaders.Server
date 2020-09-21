using System.Data;
using BeltsAndLeaders.Server.Data.Extensions;
using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(2020062303)]
    public class CreateAchievementsTable2020062303 : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists
            (
                "Achievements",
                table => table
                    .WithColumn("Id").AsBinary(16).PrimaryKey()
                    .WithColumn("UserId").AsBinary(16)
                    .WithColumn("MaturityLevelId").AsBinary(16)
                    .WithColumn("AchievementDate").AsBinary(16)
                    .WithColumn("Comment").AsFixedLengthString(1000)
                    .WithColumn("CreatedAt").AsInt64()
                    .WithColumn("UpdatedAt").AsInt64().Nullable()
            );

            Create.ForeignKey()
                .FromTable("Achievements").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);

            Create.ForeignKey()
                .FromTable("Achievements").ForeignColumn("MaturityLevelId")
                .ToTable("MaturityLevels").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("Achievements");
        }
    }
}
