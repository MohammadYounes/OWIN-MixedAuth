/// <copyright file="IMixedAuthProvider.cs" auther="Mohammad Younes">
/// 
/// Based on IGoogleAuthenticationProvider
/// https://katanaproject.codeplex.com/SourceControl/latest#src/Microsoft.Owin.Security.Google/Provider/IGoogleAuthenticationProvider.cs
/// 
/// Copyright 2014 Mohammad Younes. 
/// https://github.com/MohammadYounes/Owin-MixedAuth
/// 
/// Released under the MIT license
/// http://opensource.org/licenses/MIT
///
/// </copyright>

using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MohammadYounes.Owin.Security.MixedAuth
{
    /// <summary>
    /// Specifies callback methods which the <see cref="MixedAuthMiddleware"></see> invokes to enable developer control over the authentication process. />
    /// </summary>
    public interface IMixedAuthProvider
    {

        /// <summary>
        /// Invoked to get the LogonUserIdentity
        /// </summary>
        /// <param name="context">The OWIN environment contenxt</param>
        /// <returns>The <see cref="WindowsIdentity"/> for the current user.</returns>
        WindowsIdentity GetLogonUserIdentity(IOwinContext context);

        /// <summary>
        /// Invoked to import custom claims
        /// </summary>
        /// <param name="identity">The <see cref="WindowsIdentity"/> for the authenticated user.</param>
        /// <returns></returns>
        List<Claim> ImportClaims(WindowsIdentity identity);

        /// <summary>
        /// Invoked whenever MixedAuth succesfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="System.Security.Claims.ClaimsIdentity"/>.</param>
        /// <returns>A <see cref="Task"/> representing the completed operation.</returns>
        void ApplyRedirect(MixedAuthApplyRedirectContext context);

        /// <summary>
        /// Called when a Challenge causes a redirect to authorize endpoint in the MixedAuth middleware
        /// </summary>
        /// <param name="context">Contains redirect URI and <see cref="AuthenticationProperties"/> of the challenge </param>
        Task Authenticated(MixedAuthAuthenticatedContext context);
    }
}
