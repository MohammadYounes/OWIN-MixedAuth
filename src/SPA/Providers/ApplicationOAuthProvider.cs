using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace SPA.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                var httpRequest = ((System.Web.HttpContextBase)context.OwinContext.Environment["System.Web.HttpContextBase"]).Request;
                var path = string.Format("{0}/", httpRequest.ApplicationPath.TrimEnd('/'));

                Uri expectedRootUri = new Uri(context.Request.Uri, path);

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
                else if (context.ClientId == "web")
                {
                    var expectedUri = new Uri(context.Request.Uri, path);
                    context.Validated(expectedUri.AbsoluteUri);
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}