using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace myfoodapp.Hub.Providers
{
    public class ExternalCustomCORSProvider
    {
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ExternalCustomCORSAttribute : Attribute, ICorsPolicyProvider
    {
        private CorsPolicy _policy;

        public ExternalCustomCORSAttribute()
        {
            // Create a CORS policy.
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };

            // Add allowed origins
            _policy.Origins.Add("https://neosideadesign.sharepoint.com"); //test
            _policy.Origins.Add("https://myfoodreconnectwithyourfood.sharepoint.com/");
            _policy.Origins.Add("https://myfood.eu/");
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }
}