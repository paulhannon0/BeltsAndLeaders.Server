using System.Data;
using BeltsAndLeaders.Server.Data.Extensions;
using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(2020062302)]
    public class CreateMaturityLevelsTable2020062302 : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists
            (
                "MaturityLevels",
                table => table
                    .WithColumn("Id").AsBinary(16).PrimaryKey()
                    .WithColumn("MaturityCategoryId").AsBinary(16)
                    .WithColumn("BeltLevel").AsString(255)
                    .WithColumn("Description").AsFixedLengthString(1000)
                    .WithColumn("CreatedAt").AsInt64()
                    .WithColumn("UpdatedAt").AsInt64().Nullable()
            );

            Create.ForeignKey()
                .FromTable("MaturityLevels").ForeignColumn("MaturityCategoryId")
                .ToTable("MaturityCategories").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("MaturityLevels");
        }
    }
}
