using System;
using System.Data;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace BeltsAndLeaders.Server.Api
{
    public static class Database
    {
        public static void Create()
        {
            using (IDbConnection connection = new MySqlConnection(ConfigurationProfile.DatabaseConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = $"CREATE DATABASE IF NOT EXISTS `{ConfigurationProfile.DatabaseName}`;";
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        public static void Migrate(IMigrationRunner migrationRunner)
        {
            migrationRunner.MigrateUp();
        }
    }
}
