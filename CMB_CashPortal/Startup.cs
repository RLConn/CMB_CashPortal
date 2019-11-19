using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMB_CashPortal.Startup))]
namespace CMB_CashPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
