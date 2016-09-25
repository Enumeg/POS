using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(POS.Portal.Startup))]
namespace POS.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
