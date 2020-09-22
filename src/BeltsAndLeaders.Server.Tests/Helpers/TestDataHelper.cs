using System;
using System.Threading.Tasks;
using BeltsAndLeaders.Server.Data.Helpers;

namespace BeltsAndLeaders.Server.Tests.Helpers
{
    public abstract class TestDataHelper
    {
        protected TestHost TestHost { get; set; }

        public TestDataHelper(TestHost testHost)
        {
            this.TestHost = testHost;
        }

        public async Task<bool> DoesRecordExist<T>(Guid id) where T : class
        {
            var record = await RepositoryHelper.GetByIdAsync<T>(id.ToByteArray());

            return record != null;
        }
    }
}
