using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using myfoodapp.Hub.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Providers
{

    public interface IAuthenticationProvider : IDisposable
    {
        bool IsApiKeyAllowed(string apiKey);
    }
    public class MyFoodApiKeyAuthenticationProvider : IAuthenticationProvider
    {
        private readonly IConfiguration _configuration;

        public MyFoodApiKeyAuthenticationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsApiKeyAllowed(string apiKey)
        {
            return _configuration.ApiKeys.Contains(apiKey);
        }

       
        public static MyFoodApiKeyAuthenticationProvider Create(IdentityFactoryOptions<IAuthenticationProvider> options, IOwinContext context)
        {
            return new MyFoodApiKeyAuthenticationProvider(context.Get<IConfiguration>());
        }

        public void Dispose()
        {
            if(_configuration!= null)
            {
                _configuration.Dispose();
            }
        }
    }
}