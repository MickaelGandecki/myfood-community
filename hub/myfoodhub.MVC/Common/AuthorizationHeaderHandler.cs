using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace myfoodapp.Hub.Common
{
    public class AuthorizationHeaderHandler
        : DelegatingHandler
    {
        private static string ApiKeyHeaderValue = WebConfigurationManager.AppSettings["apiKeyHeaderValue"];

        private static string ApiKeyClaimUser = WebConfigurationManager.AppSettings["apiKeyClaimUser"];

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IEnumerable<string> apiKeyHeaderValues = null;
            if (request.Headers.TryGetValues("X-ApiKey", out apiKeyHeaderValues))
            {
                var apiKeyHeaderValue = apiKeyHeaderValues.First();

                if(apiKeyHeaderValue == ApiKeyHeaderValue)
                {
                    var usernameClaim = new Claim(ClaimTypes.Name, ApiKeyClaimUser);
                    var identity = new ClaimsIdentity(new[] { usernameClaim }, "ApiKey");
                    var principal = new ClaimsPrincipal(identity);

                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                }

            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}