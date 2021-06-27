using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Common.Security
{
    public class Configuration : IConfiguration
    {
        private readonly HashSet<string> _apiKeys;

        public HashSet<string> ApiKeys => _apiKeys;
        public Configuration()
        {
            var section = ConfigurationManager.GetSection("apiClients") as SecurityConfigurationSection;
            _apiKeys = new HashSet<string>(section.Elements.Cast<SecurityConfigurationElement>()
                .Where(e => e.Enabled)
                .Select(e => e.ApiKey));
        }

        public static Configuration Create()
        {
            return new Configuration();
        }

        public void Dispose()
        {
        }
    }
}