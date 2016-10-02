using Microsoft.Owin;
using Owin;
using POS.Portal;

[assembly: OwinStartup(typeof(Startup))]
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
