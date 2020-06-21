using System;

namespace BeltsAndLeaders.Server.Tests
{
    public static class TestConfiguration
    {
        public static void Apply()
        {
            Environment.SetEnvironmentVariable("DATABASE_CONNECTION_STRING", "Server=192.168.99.100;Uid=root;Pwd=admin");
            // Environment.SetEnvironmentVariable("DATABASE_CONNECTION_STRING", "Server=localhost;Uid=root;Pwd=admin");
            Environment.SetEnvironmentVariable("DATABASE_NAME", "BeltsAndLeaders");
        }
    }
}
