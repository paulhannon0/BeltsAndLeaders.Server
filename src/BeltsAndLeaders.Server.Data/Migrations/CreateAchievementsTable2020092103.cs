using BeltsAndLeaders.Server.Data.Extensions;
using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(2020092103)]
    public class CreateAchievementsTable2020092103 : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists
            (
                "Achievements",
                table => table
                    .WithColumn("Id").AsCustom("BINARY(16)").PrimaryKey()
                    .WithColumn("UserId").AsCustom("BINARY(16)").ForeignKey()
                    .WithColumn("MaturityLevelId").AsCustom("BINARY(16)")
                    .WithColumn("AchievementDate").AsInt64()
                    .WithColumn("Comment").AsFixedLengthString(1000)
                    .WithColumn("CreatedAt").AsInt64()
                    .WithColumn("UpdatedAt").AsInt64().Nullable()
            );

            if (!Schema.Table("Achievements").Constraint("FK_Achievements_UserId_Users_Id").Exists())
            {
                Create.ForeignKey()
                    .FromTable("Achievements").ForeignColumn("UserId")
                    .ToTable("Users").PrimaryColumn("Id");
            }


            if (!Schema.Table("Achievements").Constraint("FK_Achievements_MaturityLevelId_MaturityLevels_Id").Exists())
            {
                Create.ForeignKey()
                    .FromTable("Achievements").ForeignColumn("MaturityLevelId")
                    .ToTable("MaturityLevels").PrimaryColumn("Id");
            }
        }

        public override void Down()
        {
            Delete.Table("Achievements");
        }
    }
}
