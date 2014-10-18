/// <copyright file="MixedAuthMiddleware.cs" auther="Mohammad Younes">
/// Copyright 2014 Mohammad Younes. 
/// https://github.com/MohammadYounes/Owin-MixedAuth
/// 
/// Released under the MIT license
/// http://opensource.org/licenses/MIT
///
/// </copyright>

using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammadYounes.Owin.Security.MixedAuth
{
    /// <summary>
    /// Mixed Authentication middleware
    /// </summary>
    public class MixedAuthMiddleware : AuthenticationMiddleware<MixedAuthOptions>
    {
        // <summary>
        /// Initializes a <see cref="MixedAuthMiddleware"/>
        /// </summary>
        /// <param name="next">The next middleware in the OWIN pipeline to invoke</param>
        /// <param name="app">The OWIN application</param>
        /// <param name="options">Configuration options for the middleware</param>
        public MixedAuthMiddleware(OwinMiddleware next, IAppBuilder app,
            MixedAuthOptions options)
            : base(next, options)
        {

            if (String.IsNullOrEmpty(Options.SignInAsAuthenticationType))
            {
                Options.SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType();
            }

            if (Options.Provider == null)
            {
                Options.Provider = new MixedAuthProvider();
            }

            if (Options.StateDataFormat == null)
            {
                IDataProtector dataProtecter = app.CreateDataProtector(
                    typeof(MixedAuthMiddleware).Namespace,
                    Options.AuthenticationType,
                    "v1"
                );
                Options.StateDataFormat = new PropertiesDataFormat(dataProtecter);
            }

            if (Options.AccessTokenFormat == null)
            {
                IDataProtector dataProtecter = app.CreateDataProtector(
                    typeof(OAuthAuthorizationServerMiddleware).Namespace,
                    "Access_Token",
                    "v1"
                );
                Options.AccessTokenFormat = new TicketDataFormat(dataProtecter);
            }

        }

        /// <summary>
        /// Provides the <see cref="AuthenticationHandler"/> object for processing authentication-related requests.
        /// </summary>
        /// <returns>An <see cref="AuthenticationHandler"/> configured with the <see cref="MixedAuthOptions"/> supplied to the constructor.</returns>
        protected override AuthenticationHandler<MixedAuthOptions> CreateHandler()
        {
            return new MixedAuthHandler();
        }
    }
}
