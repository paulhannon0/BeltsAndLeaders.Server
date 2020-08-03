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
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                    .WithColumn("MaturityCategoryId").AsInt64()
                    .WithColumn("MaturityLevel").AsByte()
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
