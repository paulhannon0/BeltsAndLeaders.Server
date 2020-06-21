using System;
using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using FluentMigrator.Infrastructure;

namespace BeltsAndLeaders.Server.Data.Extensions
{
    public static class FluentMigratorExtensions
    {
        public static IFluentSyntax CreateTableIfNotExists(
            this MigrationBase self,
            string tableName,
            Func<ICreateTableWithColumnOrSchemaOrDescriptionSyntax, IFluentSyntax> constructTableFunction,
            string schemaName = "dbo"
        )
        {
            if (!self.Schema.Schema(schemaName).Table(tableName).Exists())
            {
                return constructTableFunction(self.Create.Table(tableName));
            }
            else
            {
                return null;
            }
        }
    }
}
