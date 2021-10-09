using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using EClaim.Providers;

namespace EClaim
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the application for OAuth based flow
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthAuthorizationServer(oAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    
    }
}