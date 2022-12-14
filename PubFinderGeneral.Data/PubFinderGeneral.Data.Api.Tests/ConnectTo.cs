using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using PubFinderGeneral.Data.Api.Models.Response;
using PubFinderGeneral.Data.Store;
using PubFinderGeneral.Data.Store.Models;

namespace PubFinderGeneral.Data.Api.Tests
{
    public static partial class connect_to
    {
        public static ConnectConfig pub_finder_general_data_api()
        {
            var connectConfig = new ConnectConfig { };

            return connectConfig;
        }

        public static async Task<HttpResponseMessage> execute(this ConnectConfig config,
            Func<HttpClient, Task<HttpResponseMessage>> action)
        {

            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(sc =>
                    {
                        builder.UseEnvironment(Environments.Development);
                        builder.UseTestServer();
                        sc.RemoveAll(typeof(IPubDataStore));


                        if (config.DataStore == null)
                        {
                            var moc = new Mock<IPubDataStore>();
                            moc.Setup(s => s.GetFilteredPubData(1, 25, ""))
                            .ReturnsAsync((new List<Pub>(), 0));
                            config.DataStore = moc.Object;

                        }
                        sc.AddSingleton(config.DataStore);
                    });
                });

            using var client = application.CreateClient();

            var result = await action(client);
            return result;
        }
    }
}

