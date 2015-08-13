/// <copyright file="MixedAuthApplyRedirectContext.cs" auther="Mohammad Younes">
/// 
/// Based on GoogleApplyRedirectContext
/// https://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin.Security.Google/Provider/GoogleApplyRedirectContext.cs
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
using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohammadYounes.Owin.Security.MixedAuth
{
    /// <summary>
    /// Context passed when a Challenge causes a redirect to authorize endpoint in the MixedAuth middleware
    /// </summary>
    public class MixedAuthApplyRedirectContext : BaseContext<MixedAuthOptions>
    {
        /// <summary>
        /// Creates a new context object.
        /// </summary>
        /// <param name="context">The OWIN request context</param>
        /// <param name="options">The MixedAuth middleware options</param>
        /// <param name="properties">The authentication properties of the challenge</param>
        /// <param name="redirectUri">The initial redirect URI</param>
        public MixedAuthApplyRedirectContext(IOwinContext context, MixedAuthOptions options,
            AuthenticationProperties properties, string redirectUri)
            : base(context, options)
        {
            RedirectUri = redirectUri;
            Properties = properties;
        }

        /// <summary>
        /// Gets the URI used for the redirect operation.
        /// </summary>
        public string RedirectUri { get; private set; }

        /// <summary>
        /// Gets the authentication properties of the challenge
        /// </summary>
        public AuthenticationProperties Properties { get; private set; }
    }
}
