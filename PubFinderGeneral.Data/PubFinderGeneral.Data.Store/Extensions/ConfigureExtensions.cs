using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
