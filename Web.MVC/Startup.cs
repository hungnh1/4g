using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(baohiem.Startup))]
namespace baohiem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
