/// <copyright file="MixedAuthAuthenticatedContext.cs" auther="Mohammad Younes">
/// 
/// Based on GoogleAuthenticatedContext
/// https://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin.Security.Google/Provider/GoogleAuthenticatedContext.cs
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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MohammadYounes.Owin.Security.MixedAuth
{
    /// <summary>
    /// Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.
    /// </summary>
    public class MixedAuthAuthenticatedContext : BaseContext
    {
        /// <summary>
        /// Initializes a <see cref="MixedAuthAuthenticatedContext"/>
        /// </summary>
        /// <param name="context">The OWIN environment</param>
        /// <param name="identity">The <see cref="ClaimsIdentity"/> representing the user</param>
        /// <param name="properties">A property bag for common authentication properties</param>
        public MixedAuthAuthenticatedContext(
            IOwinContext context,
            ClaimsIdentity identity,
            AuthenticationProperties properties,
            string accessToken)
            : base(context)
        {
            this.Identity = identity;
            this.Properties = properties;
            this.AccessToken = accessToken;
        }



        /// <summary>
        /// Gets the MixedAuth access token
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="ClaimsIdentity"/> representing the user
        /// </summary>
        public ClaimsIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets a property bag for common authentication properties
        /// </summary>
        public AuthenticationProperties Properties { get; set; }



    }
}
