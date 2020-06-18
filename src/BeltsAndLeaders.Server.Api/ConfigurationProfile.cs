using System;

namespace BeltsAndLeaders.Server.Api
{
    public static class ConfigurationProfile
    {
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");

        public static readonly string DatabaseConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");

        public static readonly string SelectedDatabaseConnectionString = $"{DatabaseConnectionString};Database={DatabaseName};";
    }
}
