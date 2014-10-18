using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebForms.Startup))]
namespace WebForms
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
