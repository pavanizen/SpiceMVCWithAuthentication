using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpiceMVCWithAuthentication.Startup))]
namespace SpiceMVCWithAuthentication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
