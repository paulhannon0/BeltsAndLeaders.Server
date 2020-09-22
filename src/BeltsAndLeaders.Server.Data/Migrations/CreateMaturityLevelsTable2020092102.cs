using BeltsAndLeaders.Server.Data.Extensions;
using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(2020092102)]
    public class CreateMaturityLevelsTable2020092102 : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists
            (
                "MaturityLevels",
                table => table
                    .WithColumn("Id").AsCustom("BINARY(16)").PrimaryKey()
                    .WithColumn("MaturityCategoryId").AsCustom("BINARY(16)").ForeignKey()
                    .WithColumn("BeltLevel").AsString(255)
                    .WithColumn("Description").AsFixedLengthString(1000)
                    .WithColumn("CreatedAt").AsInt64()
                    .WithColumn("UpdatedAt").AsInt64().Nullable()
            );

            if (!Schema.Table("MaturityLevels").Constraint("FK_MaturityLevels_MaturityCategoryId_MaturityCategories_Id").Exists())
            {
                Create.ForeignKey()
                    .FromTable("MaturityLevels").ForeignColumn("MaturityCategoryId")
                    .ToTable("MaturityCategories").PrimaryColumn("Id");
            }
        }

        public override void Down()
        {
            Delete.Table("MaturityLevels");
        }
    }
}
