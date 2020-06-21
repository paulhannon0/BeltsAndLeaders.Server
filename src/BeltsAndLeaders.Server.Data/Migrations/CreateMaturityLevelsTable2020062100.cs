using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(2020062100)]
    public class CreateMaturityLevelsTable2020062100 : Migration
    {
        public override void Up()
        {
            Create.Table("MaturityLevels")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("MaturityCategoryId").AsInt64().ForeignKey("MaturityCategories", "Id")
                .WithColumn("MaturityLevel").AsByte()
                .WithColumn("Description").AsFixedLengthString(1000)
                .WithColumn("CreatedAt").AsInt64()
                .WithColumn("UpdatedAt").AsInt64().Nullable();
        }

        public override void Down()
        {
            Delete.Table("MaturityLevels");
        }
    }
}
