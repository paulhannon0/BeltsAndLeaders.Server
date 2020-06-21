using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(2020062101)]
    public class CreateAchievementsTable20200618184301 : Migration
    {
        public override void Up()
        {
            Create.Table("Achievements")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt64().ForeignKey("Users", "Id")
                .WithColumn("MaturityLevelId").AsInt64().ForeignKey("MaturityLevels", "Id")
                .WithColumn("AchievementDate").AsInt64()
                .WithColumn("Comment").AsFixedLengthString(1000)
                .WithColumn("CreatedAt").AsInt64()
                .WithColumn("UpdatedAt").AsInt64().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Achievements");
        }
    }
}
