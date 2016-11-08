using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EToolsWebsite.Startup))]
namespace EToolsWebsite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
