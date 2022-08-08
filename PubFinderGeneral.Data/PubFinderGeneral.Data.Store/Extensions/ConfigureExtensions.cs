using Microsoft.Extensions.DependencyInjection;

namespace PubFinderGeneral.Data.Store.Extensions
{
    public static class ConfigureExtensions
    {
        public static IServiceCollection RegisterPubCSVFiles(this IServiceCollection serviceCollection, string fileName, string path)
        {
            serviceCollection.AddSingleton<IPubDataStore>(new CsvPubDataStore(path, fileName));
            return serviceCollection;
        }
    }
}
