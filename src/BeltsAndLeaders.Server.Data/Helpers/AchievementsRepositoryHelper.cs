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
    public static class AchievementsRepositoryHelper
    {
        public static async Task<int> GetAchievementCountByUserIdAndBeltColour(Guid userId, string beltLevel)
        {
            using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")))
            {
                connection.Open();
                RepositoryHelper.UseDatabase(connection);

                var count = await connection.ExecuteScalarAsync
                (
                    $@"
                        SELECT COUNT(*)
                        FROM (
                            SELECT `Achievements`.`MaturityLevelId`
                            FROM `Achievements`
                            JOIN `MaturityLevels`
                            ON `MaturityLevels`.`Id` = `Achievements`.`MaturityLevelId`
                            WHERE `Achievements`.`UserId` = @UserId
                            AND `MaturityLevels`.`BeltLevel` = @BeltLevel
                            GROUP BY `Achievements`.`MaturityLevelId`
                        ) `Records`;
                    ",
                    new
                    {
                        UserId = userId,
                        BeltLevel = beltLevel
                    }

                );

                connection.Close();
                return Convert.ToInt32(count);
            }
        }

        public static async Task<int> GetUniqueAchievementsCountByUserId(Guid userId)
        {
            using (var connection = new MySqlConnection(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")))
            {
                connection.Open();
                RepositoryHelper.UseDatabase(connection);

                var count = await connection.ExecuteScalarAsync
                (
                    $@"
                        SELECT COUNT(*)
                        FROM (
                            SELECT `Achievements`.`MaturityLevelId`
                            FROM `Achievements`
                            WHERE `Achievements`.`UserId` = @UserId
                            GROUP BY `Achievements`.`MaturityLevelId`
                        ) `Records`;
                    ",
                    new
                    {
                        UserId = userId
                    }

                );

                connection.Close();
                return Convert.ToInt32(count);
            }
        }
    }
}
