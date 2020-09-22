using BeltsAndLeaders.Server.Data.Extensions;
using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(2020092101)]
    public class CreateMaturityCategoriesTable2020092101 : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists
            (
                "MaturityCategories",
                table => table
                    .WithColumn("Id").AsCustom("BINARY(16)").PrimaryKey()
                    .WithColumn("Name").AsFixedLengthString(255)
                    .WithColumn("CreatedAt").AsInt64()
                    .WithColumn("UpdatedAt").AsInt64().Nullable()
            );
        }

        public override void Down()
        {
            Delete.Table("MaturityCategories");
        }
    }
}
