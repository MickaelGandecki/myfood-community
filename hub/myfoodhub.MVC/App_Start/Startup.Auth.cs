using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.ApiKey;
using Microsoft.Owin.Security.ApiKey.Contexts;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using myfoodapp.Hub.Common.Security;
using myfoodapp.Hub.Models;
using myfoodapp.Hub.Providers;
using Owin;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myfoodapp.Hub
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager, security configuration and api security provider to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
           
            // Add Role Manager
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            //Inject configuration and ApiSecurityProvider
            app.CreatePerOwinContext<IConfiguration>(myfoodapp.Hub.Common.Security.Configuration.Create);
            app.CreatePerOwinContext<IAuthenticationProvider>(MyFoodApiKeyAuthenticationProvider.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                CookieSecure = CookieSecureOption.Always
            });

            //enable authentication  by using api key (used specially for opendata web Api controller with authentication enabled)
            app.UseApiKeyAuthentication(new ApiKeyAuthenticationOptions()
            {
                Provider = new ApiKeyAuthenticationProvider()
                {
                    OnValidateIdentity = ValidateIdentity,
                    OnGenerateClaims = GenerateClaims
                }, AuthenticationType = "Bearer"
            });

            //// Configure the application for OAuth based flow
            //PublicClientId = "self";
            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/Token"),
            //    Provider = new ApplicationOAuthProvider(PublicClientId),
            //    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
            //    // In production mode set AllowInsecureHttp = false
            //    AllowInsecureHttp = true
            //};

            //// Enable the application to use bearer tokens to authenticate users
            //app.UseOAuthBearerTokens(OAuthOptions);
        }

        private Task ValidateIdentity(ApiKeyValidateIdentityContext context)
        {
            var authenticationService = context.OwinContext.Environment["AspNet.Identity.Owin:myfoodapp.Hub.Providers.IAuthenticationProvider, myfoodapp.Hub, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"]
                as IAuthenticationProvider;

            if (authenticationService.IsApiKeyAllowed(context.ApiKey))
            {
                context.Validate();
            }

            return Task.FromResult(true);
        }

        private Task<IEnumerable<Claim>> GenerateClaims(ApiKeyGenerateClaimsContext context)
        {
            return Task.FromResult((IEnumerable<Claim>)
                new[] {
                    new Claim(ClaimTypes.Name, context.ApiKey)
                });
        }
    }
}
