using System.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace PubFinderGeneral.Data.Api.Tests
{
    public abstract class BddAsyncTest : IAsyncLifetime
    {
        public Task DisposeAsync() => Task.CompletedTask;

        public Task InitializeAsync()
        {
            return Setup();
        }

        protected abstract Task Setup();
    }
}
