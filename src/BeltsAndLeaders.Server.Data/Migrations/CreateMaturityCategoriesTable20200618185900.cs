using BeltsAndLeaders.Server.Data.Extensions;
using FluentMigrator;

namespace BeltsAndLeaders.Server.Data.Migrations
{
    [Migration(20200618185900)]
    public class CreateMaturityCategoriesTable20200618185900 : Migration
    {
        public override void Up()
        {
            this.CreateTableIfNotExists
            (
                "MaturityCategories",
                table => table
                    .WithColumn("Id").AsInt64().PrimaryKey().Identity()
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
