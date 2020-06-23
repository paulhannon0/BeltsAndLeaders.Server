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
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("UserId").AsInt64()
                    .WithColumn("MaturityLevelId").AsInt64()
                    .WithColumn("AchievementDate").AsInt64()
                    .WithColumn("Comment").AsFixedLengthString(1000)
                    .WithColumn("CreatedAt").AsInt64()
                    .WithColumn("UpdatedAt").AsInt64().Nullable()
            );

            Create.ForeignKey()
                .FromTable("Achievements").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id");

            Create.ForeignKey()
                .FromTable("Achievements").ForeignColumn("MaturityLevelId")
                .ToTable("MaturityLevels").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Achievements");
        }
    }
}
