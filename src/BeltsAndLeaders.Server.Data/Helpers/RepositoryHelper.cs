using System;
using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using BeltsAndLeaders.Server.Data.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Dapper;

namespace BeltsAndLeaders.Server.Data.Helpers
{
    public static class RepositoryHelper
    {
        public static async Task InsertAsync<T>(T record) where T : class
        {
            using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")))
            {
                connection.Open();
                UseDatabase(connection);

                var castRecord = record as IRecord;
                castRecord.CreatedAt = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                await connection.InsertAsync<T>(castRecord as T);

                connection.Close();
            }
        }

        public static async Task<T> GetByIdAsync<T>(byte[] id) where T : class
        {
            using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")))
            {
                connection.Open();
                UseDatabase(connection);

                var record = await connection.GetAsync<T>(id);

                connection.Close();

                return record;
            }
        }

        public static async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
        {
            using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")))
            {
                connection.Open();
                UseDatabase(connection);

                var records = await connection.GetAllAsync<T>();

                connection.Close();

                return records;
            }
        }

        public static async Task<IEnumerable<T>> GetByNonKeyIdValue<T>(string tableName, string columnName, byte[] value) where T : class
        {
            using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")))
            {
                connection.Open();
                UseDatabase(connection);

                var records = await connection.QueryAsync<T>
                (
                    $@"
                        SELECT *
                        FROM `{tableName}`
                        WHERE `{columnName}` = @Value
                    ",
                    new
                    {
                        Value = value
                    }

                );

                connection.Close();
                return records;
            }
        }

        public static async Task UpdateAsync<T>(T record) where T : class
        {
            using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")))
            {
                connection.Open();
                UseDatabase(connection);

                var castRecord = record as IRecord;

                await connection.UpdateAsync<T>(castRecord as T);
                connection.Close();
            }
        }

        public static async Task DeleteAsync<T>(T record) where T : class
        {
            using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")))
            {
                connection.Open();
                UseDatabase(connection);

                var castRecord = record as IRecord;

                await connection.DeleteAsync<T>(castRecord as T);
                connection.Close();
            }
        }

        public static void UseDatabase(IDbConnection connection)
        {
            var command = connection.CreateCommand();

            command.CommandText = "USE `BeltsAndLeaders`;";
            command.ExecuteNonQuery();
        }
    }
}
