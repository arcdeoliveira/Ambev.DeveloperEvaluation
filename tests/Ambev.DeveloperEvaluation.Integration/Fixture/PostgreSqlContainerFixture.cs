using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Fixture
{
    public class PostgreSqlContainerFixture : IAsyncLifetime
    {
        public string ConnectionString { get; private set; }

        //private readonly PostgreSqlTestcontainer _postgresContainer;
        public Task DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
