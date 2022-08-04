using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using PubFinderGeneral.Data.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubFinderGeneral.Data.Api.Tests
{
    public static partial class connect_to
    {
        public class ConnectConfig
        {
            public IPubDataStore DataStore { get; set; } = null;
        }

        public static ConnectConfig with_a_data_store(this ConnectConfig config, IPubDataStore dataStore)
        {
            config.DataStore = dataStore;
            return config;
        }

        internal class TestConfiguration : Dictionary<string, string>, IConfigurationSection
        {
            public IConfigurationSection GetSection(string key)
            {
                if (ContainsKey(key))
                {
                    return new TestConfiguration { Key = key, Value = this[key] };
                }

                return new TestConfiguration();
            }


            public IEnumerable<IConfigurationSection> GetChildren()
            {
                throw new NotImplementedException();
            }

            public IChangeToken GetReloadToken()
            {
                throw new NotImplementedException();
            }

            public string Key { get; set; }
            public string Path { get; }
            public string Value { get; set; }
        }
    }
}
