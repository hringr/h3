using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hringr.Startup))]
namespace hringr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
