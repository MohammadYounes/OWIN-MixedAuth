/// <copyright file="AppBuilderExtensions.cs" auther="Mohammad Younes">
/// Copyright 2014 Mohammad Younes. 
/// https://github.com/MohammadYounes/Owin-MixedAuth
/// 
/// Released under the MIT license
/// http://opensource.org/licenses/MIT
///
/// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security.Cookies;
using MohammadYounes.Owin.Security.MixedAuth;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Adds a mixed-authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The IAppBuilder passed to your configuration method</param>
        /// <param name="options">An options class that controls the middleware behavior</param>
        /// <param name="cookieOptions">An options class that controls the middleware behavior</param>
        /// <returns>The original app parameter</returns>
        public static IAppBuilder UseMixedAuth(this IAppBuilder app,
            MixedAuthOptions options,
            CookieAuthenticationOptions cookieOptions)
        {
            if (app == null)
                throw new ArgumentNullException("app");
            if (options == null)
                throw new ArgumentNullException("options");
            if (cookieOptions == null)
                throw new ArgumentNullException("cookieOptions");

            options.CookieOptions = cookieOptions;

            app.Use(typeof(MixedAuthMiddleware), app, options);

            app.UseStageMarker(PipelineStage.PostAuthenticate);

            return app;
        }

        /// <summary>
        /// Adds a mixed-authentication middleware to your web application pipeline.
        /// </summary>
        /// <param name="app">The IAppBuilder passed to your configuration method</param>
        /// <param name="cookieOptions">An options class that controls the middleware behavior</param>
        /// <returns>The original app parameter</returns>
        public static IAppBuilder UseMixedAuth(this IAppBuilder app, CookieAuthenticationOptions cookieOptions)
        {
            return app.UseMixedAuth(new MixedAuthOptions(), cookieOptions);
        }

    }
}
