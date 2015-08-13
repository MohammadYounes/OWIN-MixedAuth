/// <copyright file="MixedAuthOptions.cs" auther="Mohammad Younes">
/// 
/// Based on OAuthAuthorizationServerOptions and GoogleAuthenticationOptions
/// https://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin.Security.OAuth/OAuthAuthorizationServerOptions.cs
/// https://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin.Security.Google/GoogleAuthenticationOptions.cs
/// 
/// Copyright 2014 Mohammad Younes. 
/// https://github.com/MohammadYounes/Owin-MixedAuth
/// 
/// Released under the MIT license
/// http://opensource.org/licenses/MIT
///
/// </copyright>

using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammadYounes.Owin.Security.MixedAuth
{
    /// <summary>
    /// Contains the options used by the MixedAuthMiddleware
    /// </summary>
    public class MixedAuthOptions : AuthenticationOptions
    {
        /// <summary>
        /// Create an instance of the options initialized with the default values
        /// </summary>
        public MixedAuthOptions()
            : base("Windows")
        {
            Caption = MixedAuthConstants.DefaultAuthenticationType;
            CallbackPath = new PathString("/MixedAuth");
            ClientId = "MixedAuth";
            AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive;
        }

        /// <summary>
        /// Gets or sets the MixedAuth-assigned client id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Get or sets the text that the user can display on a sign in user interface.
        /// </summary>
        public string Caption
        {
            get { return Description.Caption; }
            set { Description.Caption = value; }
        }

        /// <summary>
        /// Gets or sets the type used to secure data handled by the middleware.
        /// </summary>
        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }


        /// <summary>
        /// The data format used to un-protect the information contained in the access token.
        /// If not provided by the application the default data protection provider depends on the host server. 
        /// The SystemWeb host on IIS will use ASP.NET machine key data protection, and HttpListener and other self-hosted
        /// servers will use DPAPI data protection. If a different access token
        /// provider or format is assigned, a compatible instance must be assigned to the OAuthAuthorizationServerOptions.AccessTokenProvider 
        /// and OAuthAuthorizationServerOptions.AccessTokenFormat of the authorization server.
        /// </summary>
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; set; }


        /// <summary>
        /// Gets or sets the Cookies authentication options used by CookiesAuthenticationMiddleware.
        /// </summary>
        public CookieAuthenticationOptions CookieOptions { get; set; }

        /// <summary>
        /// The request path within the application's base path where the user-agent will be returned.
        /// The middleware will process this request when it arrives.
        /// Default value is "/MixedAuth".
        /// </summary>
        public PathString CallbackPath { get; set; }


        /// <summary>
        /// Gets or sets the name of another authentication middleware which will be responsible for actually issuing a user <see cref="System.Security.Claims.ClaimsIdentity"/>.
        /// </summary>
        public string SignInAsAuthenticationType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IMixedAuthProvider"/> used to handle authentication events.
        /// </summary>
        public IMixedAuthProvider Provider { get; set; }
    }
}
